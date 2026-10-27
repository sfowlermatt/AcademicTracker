using AcademicTracker.ViewModels;

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
}