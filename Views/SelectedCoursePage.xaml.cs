using AcademicTracker.ViewModels;
using AcademicTracker.Models;

namespace AcademicTracker.Views;

public partial class SelectedCoursePage : ContentPage
{
	private readonly SelectedCourseViewModel _viewModel;
    public SelectedCoursePage(SelectedCourseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCourseCommand.Execute(null);
    }

    private async void OnAssessmentTapped(object sender, EventArgs e)
    {
        if (sender is not Element { BindingContext: Assessments assessment })
            return;
        await _viewModel.EditAssessmentCommand.ExecuteAsync(assessment);
    }
}