using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AcademicTracker.Models;
using AcademicTracker.Services;

namespace AcademicTracker.ViewModels
{
    [QueryProperty(nameof(TermId), "termId")]
    public partial class TermEditViewModel : ObservableObject
    {
        private readonly ITerms _termService;
        private Term? _existingTerm;

        public TermEditViewModel(ITerms termService)
        {
            _termService = termService;
        }

        [ObservableProperty]
        private string? termId;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private DateTime startDate = DateTime.Today;

        [ObservableProperty]
        private DateTime endDate = DateTime.Today.AddMonths(3);

        [ObservableProperty]
        private string errorMessage = string.Empty;

        partial void OnTermIdChanged(string? value)
        {
            if (int.TryParse(value, out int id))
            {
                LoadTerm(id);
            }
        }

        [RelayCommand]
        private async Task LoadTerm(int id)
        {
            _existingTerm = await _termService.GetTermByIdAsync(id);
            if (_existingTerm != null)
            {
                title = _existingTerm.Title;
                startDate = _existingTerm.StartDate;
                endDate = _existingTerm.EndDate;
            }
        }

        [RelayCommand]
        private async Task SaveTerm()
        {

            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(title))
            {
                ErrorMessage = "Title is required.";
                return;
            }
            if (startDate > endDate)
            {
                ErrorMessage = "Start date cannot be after end date.";
                return;
            }
            var termToSave = _existingTerm ?? new Term();
            termToSave.Title = title.Trim();
            termToSave.StartDate = startDate;
            termToSave.EndDate = endDate;
            await _termService.SaveTermAsync(termToSave);
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Cancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
