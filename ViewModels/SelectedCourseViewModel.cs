using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AcademicTracker.Models;
using AcademicTracker.Services;

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

        public ObservableCollection<Assessments> Assessments { get; } = new();

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
                selectedCourse = course;
                var term = await _termService.GetTermByIdAsync(course.TermId);
                termTitle = term?.Title ?? string.Empty;
                var assessments = await _assessmentService.GetAssessmentsByCourseIdAsync(course.Id);
                Assessments.Clear();
                foreach (var assessment in assessments)
                {
                    Assessments.Add(assessment);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them)
                Console.WriteLine($"Error loading course details: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void EditCourse()
        {
            System.Diagnostics.Debug.WriteLine($"Edit course with ID: {selectedCourse?.Name}");
        }
    }
}
