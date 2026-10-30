using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AcademicTracker.Models;
using AcademicTracker.Services;
using AcademicTracker.Views;

namespace AcademicTracker.ViewModels
{
    [QueryProperty(nameof(CourseId), "courseId")]
    public partial class SelectedCourseViewModel : ObservableObject
    {
        private readonly ICourses _courseService;
        private readonly ITerms _termService;
        private readonly IAssessments _assessmentService;

        public SelectedCourseViewModel(ICourses courseService, ITerms termService, IAssessments assessmentService)
        {
            _courseService = courseService;
            _termService = termService;
            _assessmentService = assessmentService;
        }

        [ObservableProperty]
        private string? courseId;

        [ObservableProperty]
        private Course? selectedCourse;

        [ObservableProperty]
        private string termTitle = string.Empty;

        [ObservableProperty]
        private ObservableCollection<Assessments> assessmentCollection = new();

        [ObservableProperty]
        private bool isBusy;

        partial void OnCourseIdChanged(string? value)
        {
            LoadCourseCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadCourseAsync()
        {
            if (IsBusy || !int.TryParse(CourseId, out var id))
                return;
            try
            {
                IsBusy = true;
                var course = await _courseService.GetCourseByIdAsync(id);
                if (course is null)
                    return;
                SelectedCourse = course;
                var term = await _termService.GetTermByIdAsync(course.TermId);
                TermTitle = term?.Title ?? string.Empty;

                var loadedAssessments = await _assessmentService.GetAssessmentsByCourseIdAsync(course.Id);
                AssessmentCollection = new ObservableCollection<Assessments>();
                AssessmentCollection = new ObservableCollection<Assessments>(loadedAssessments);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them)
                System.Diagnostics.Debug.WriteLine($"Error loading course: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task AddAssessment()
        {
            if (SelectedCourse is null)
                return;
            await Shell.Current.GoToAsync($"{nameof(AssessmentEditView)}?courseId={SelectedCourse.Id}");
        }

        [RelayCommand]
        private async Task EditAssessment(Assessments assessment)
        {
            if (assessment is null)
                return;
            await Shell.Current.GoToAsync($"{nameof(AssessmentEditView)}?assessmentId={assessment.Id}");
        }

        [RelayCommand]
        private async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task EditCourse()
        {
            if (SelectedCourse is null)
                return;
            await Shell.Current.GoToAsync($"{nameof(CourseEditView)}?courseId={SelectedCourse.Id}");
        }
    }
}
