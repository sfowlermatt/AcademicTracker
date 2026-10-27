using AcademicTracker.Models;
using AcademicTracker.ViewModels;

namespace AcademicTracker.Views;

public partial class SelectedTermPage : ContentPage
{
    private readonly SelectedTermViewModel _viewModel;

    public SelectedTermPage(SelectedTermViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private async void OnCourseTapped(object sender, TappedEventArgs e)
    {
        if (sender is not Element { BindingContext: Course course })
            return;

        await _viewModel.OpenCourseCommand.ExecuteAsync(course);
    }

    private async void onBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
          _viewModel.LoadTermDetailsCommand.Execute(null);
    }
}