using AcademicTracker.ViewModels;

namespace AcademicTracker.Views;

public partial class SelectedCoursePage : ContentPage
{
	public SelectedCoursePage(SelectedCourseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}