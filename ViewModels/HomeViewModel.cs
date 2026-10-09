using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using AcademicTracker.Services;
using AcademicTracker.Models;
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
        private async
    }
}
