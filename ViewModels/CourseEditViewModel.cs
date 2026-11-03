using AcademicTracker.Models;
using AcademicTracker.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;


namespace AcademicTracker.ViewModels
{
    [QueryProperty(nameof(CourseId), "courseId")]
    [QueryProperty(nameof(TermId), "termId")]
    public partial class CourseEditViewModel : ObservableObject
    {
        private readonly ICourses _courseService;
        private readonly IAssessments _assessmentService;
        private readonly INotifications _notificationService;
        private Course? _existingCourse;
        public CourseEditViewModel(ICourses courseService, IAssessments assessmentService, INotifications notificationService)
        {
            _courseService = courseService;
            _assessmentService = assessmentService;
            _notificationService = notificationService;
        }

        public IReadOnlyList<Status> StatusOptions { get; } = Enum.GetValues<Status>();

        [ObservableProperty]
        private string? courseId;
        [ObservableProperty]
        private string? termId;
        [ObservableProperty]
        private string pageTitle = "Add Course";
        [ObservableProperty]
        private string name = string.Empty;
        [ObservableProperty]
        private DateTime startDate = DateTime.Today;
        [ObservableProperty]
        private DateTime endDate = DateTime.Today.AddMonths(1);
        [ObservableProperty]
        private Status status = Status.InProgress;
        [ObservableProperty]
        private string notes = string.Empty;
        [ObservableProperty]
        private string instructorName = string.Empty;
        [ObservableProperty]
        private string instructorPhone = string.Empty;
        [ObservableProperty]
        private string instructorEmail = string.Empty;
        [ObservableProperty]
        private string errorMessage = string.Empty;
        [ObservableProperty]
        private bool isEditing;
        [ObservableProperty]
        private bool notificationsEnabled;

        partial void OnCourseIdChanged(string? value)
        {
            LoadCourseCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadCourseAsync()
        {
            if (!int.TryParse(CourseId, out int courseId))
                return;

            var course = await _courseService.GetCourseByIdAsync(courseId);
            if (course is null)
                return;
            
            _existingCourse = course;
            IsEditing = true;
            NotificationsEnabled = course.NotificationsEnabled;
            PageTitle = "Edit Course";
            Name = course.Name;
            StartDate = course.StartDate;
            EndDate = course.EndDate;
            Status = course.Status;
            Notes = course.Notes;
            InstructorName = course.InstructorName;
            InstructorPhone = course.InstructorPhone;
            InstructorEmail = course.InstructorEmail;
        }

        [RelayCommand]
        private async Task SaveCourse()
        {
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Course name is required.";
                return;
            }

            if (StartDate > EndDate)
            {
                ErrorMessage = "Start date cannot be after end date.";
                return;
            }

            if(string.IsNullOrWhiteSpace(InstructorName))
            {
                ErrorMessage = "Instructor name is required.";
                return;
            }

            if (string.IsNullOrWhiteSpace(InstructorPhone) || !System.Text.RegularExpressions.Regex.IsMatch(InstructorPhone, @"^\d{3}-\d{3}-\d{4}$"))
            {
                ErrorMessage = "Instructor phone number must be in the format XXX-XXX-XXXX.";
                return;
            }

            if (string.IsNullOrWhiteSpace(InstructorEmail) && (!InstructorEmail.Contains("@") && !InstructorEmail.Contains(".edu")))

            {
                ErrorMessage = "Instructor email cannot be blank and must contain @ and .edu";
                return;
            }

            Course course;
            if (_existingCourse is not null)
            {
                course = _existingCourse;
            }

            else
            {
                if(!int.TryParse(termId, out var parentTermId))
                {
                    ErrorMessage = "This course isn't linked to a term";
                }
                course = new Course { TermId = parentTermId };

                var termCourses = await _courseService.GetCoursesByTermIdAsync(parentTermId);
                if(termCourses.Count >= 6)
                {
                    ErrorMessage = "This term already has the maximum number of courses (6).";
                    return;
                }
            }

            course.Name = Name.Trim();
            course.StartDate = StartDate;
            course.EndDate = EndDate;
            course.Status = Status;
            course.Notes = Notes;
            course.InstructorName = InstructorName.Trim();
            course.InstructorPhone = InstructorPhone.Trim();
            course.InstructorEmail = InstructorEmail.Trim();
            course.NotificationsEnabled = NotificationsEnabled;

            await _courseService.SaveCourseAsync(course);
            await _notificationService.CourseStartNotification(course);
            await _notificationService.CourseEndNotification(course);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task DeleteCourseAsync()
        {
            if (_existingCourse is null)
                return;
            //await _notificationService.CancelAssessmentNotifications(assessment.Id);
            await _notificationService.CancelCourseNotifications(_existingCourse.Id);
            // Delete all assessments associated with this course
            await _assessmentService.DeleteAssessmentByCourseIdAsync(_existingCourse.Id);
            // Delete the course itself
            await _courseService.DeleteCourseAsync(_existingCourse.Id);
            await Shell.Current.GoToAsync("../..");
        }
    }
}
