using AcademicTracker.ViewModels;

namespace AcademicTracker.Views;

public partial class TermEditPage : ContentPage
{
	public TermEditPage(TermEditViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}