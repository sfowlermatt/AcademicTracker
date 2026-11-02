using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AcademicTracker.Models;
using AcademicTracker.Services;

namespace AcademicTracker.ViewModels
{
    [QueryProperty(nameof(AssessmentId), "assessmentId")]
    [QueryProperty(nameof(CourseId), "courseId")]
    public partial class AssessmentEditViewModel : ObservableObject
    {
        private readonly IAssessments _assessmentService;
        private readonly INotifications _notificationService;
        private Assessments? _existingAssessment;

        public AssessmentEditViewModel(IAssessments assessmentService, INotifications notificationService)
        {
            _assessmentService = assessmentService;
            _notificationService = notificationService;
            
        }

        public IReadOnlyList<AssessmentType> AssessmentTypeOptions { get; } = Enum.GetValues<AssessmentType>();

        [ObservableProperty]
        private int assessmentId;

        [ObservableProperty]
        private string? courseId;

        [ObservableProperty]
        private string pageTitle = "Add Assessment";

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private AssessmentType assessType = AssessmentType.Objective;

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;

        [ObservableProperty]
        private DateTime endDate = DateTime.Today.AddMonths(3);

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private bool notificationsEnabled;

        partial void OnAssessmentIdChanged(int value)
        {
            LoadAssessmentCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadAssessmentAsync()
        {
            if (!int.TryParse(AssessmentId.ToString(), out int assessmentId))
                return; 

            var assessment = await _assessmentService.GetAssessmentByIdAsync(assessmentId);
            if (assessment is null)
                return;

            _existingAssessment = assessment;
            IsEditing = true;
            NotificationsEnabled = assessment.NotificationsEnabled;
            PageTitle = "Edit Assessment";

            Name = assessment.Name;
            AssessType = assessment.Type;
            StartDate = assessment.StartDate;
            EndDate = assessment.EndDate;
            //CourseId = assessment.CourseId;
        }

        [RelayCommand]
        private async Task SaveAssessmentAsync()
        {
            ErrorMessage = string.Empty;

            if(string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Assessment name is required.";
                return;
            }

            int parentCourseId;
            if (_existingAssessment is not null)
            {
                parentCourseId = _existingAssessment.CourseId;
            }
            else if (!int.TryParse(CourseId, out parentCourseId))
                {
                    ErrorMessage = "Invalid Course ID.";
                    return;
                }
                var siblingsAssessments = await _assessmentService.GetAssessmentsByCourseIdAsync(parentCourseId);
                int currentId = _existingAssessment?.Id ?? 0;

                if (siblingsAssessments.Any(a => a.Type == AssessType && a.Id != currentId))
                {
                    ErrorMessage = $"This course already has a {AssessType} assessment.";
                    return;
                }

            var assessment = _existingAssessment ?? new Assessments { CourseId = parentCourseId };
            assessment.Name = Name.Trim();
            assessment.Type = AssessType;
            assessment.StartDate = StartDate;
            assessment.EndDate = EndDate;
            assessment.NotificationsEnabled = NotificationsEnabled;
            //assessment.CourseId = CourseId;

            await _assessmentService.SaveAssessmentAsync(assessment);
            await _notificationService.AssessmentStartNotification(assessment);
            await _notificationService.AssessmentEndNotification(assessment);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task DeleteAssessmentAsync()
        {
            if (_existingAssessment is null)
                return;
            await _notificationService.CancelAssessmentNotifications(_existingAssessment.Id);
            await _assessmentService.DeleteAssessmentAsync(_existingAssessment.Id);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
