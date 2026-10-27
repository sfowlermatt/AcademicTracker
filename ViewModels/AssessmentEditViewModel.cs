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
    public partial class AssessmentEditViewModel : ObservableObject
    {
        private readonly IAssessments _assessmentService;

        public AssessmentEditViewModel(IAssessments assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [ObservableProperty]
        private int assessmentId;

        [ObservableProperty]
        private int? courseId;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private AssessmentType assessType;

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;

        [ObservableProperty]
        private DateTime endDate = DateTime.Today.AddMonths(3);

        [ObservableProperty]
        private string errorMessage = string.Empty;

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
            
                Name = assessment.Name;
                AssessType = assessment.Type;
                StartDate = assessment.StartDate;
                EndDate = assessment.EndDate;
                CourseId = assessment.CourseId;
            
        }

        [RelayCommand]
        private async Task SaveAssessmentAsync()
        {
            if (!int.TryParse(AssessmentId.ToString(), out int assessmentId))
                return;

            var assessment = await _assessmentService.GetAssessmentByIdAsync(assessmentId);
            if (assessment is null)
                return;

            assessment.Name = Name;
            assessment.Type = AssessType;
            assessment.StartDate = StartDate;
            assessment.EndDate = EndDate;
            assessment.CourseId = CourseId;

            await _assessmentService.UpdateAssessmentAsync(assessment);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
