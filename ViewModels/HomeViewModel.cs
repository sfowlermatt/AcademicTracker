using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Services;
using AcademicTracker.Models;
using AcademicTracker.Views;
//using AcademicTracker.Views;

namespace AcademicTracker.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ITerms _termService;
        

        public HomeViewModel(ITerms termService)
        {
            _termService = termService;
            
        }

        public ObservableCollection<Term> Terms { get; } = new();

        [ObservableProperty]
        private bool isBusy;

        [RelayCommand]
        private async Task LoadTermAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                Terms.Clear();
                var terms = await _termService.GetTermAsync();
                foreach (var term in terms)
                {
                    Terms.Add(term);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them, show a message to the user, etc.)
                System.Diagnostics.Debug.WriteLine($"Error loading terms: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        private async Task OpenTermAsync(Term term)
        {
            if (term == null) return;
            await Shell.Current.GoToAsync($"{nameof(SelectedTermPage)}?termId={term.Id}");
        }

        [RelayCommand]
        private void AddTerm()
        {
            Shell.Current.GoToAsync(nameof(AddTerm));
        }
    }
}
