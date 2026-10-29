using AcademicTracker.ViewModels;

namespace AcademicTracker.Views;

public partial class AssessmentEditView : ContentPage
{
	public AssessmentEditView(AssessmentEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}