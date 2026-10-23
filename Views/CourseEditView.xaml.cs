using AcademicTracker.ViewModels;

namespace AcademicTracker.Views;

public partial class CourseEditView : ContentPage
{
	public CourseEditView(CourseEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}