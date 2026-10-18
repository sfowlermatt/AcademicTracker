namespace AcademicTracker.Views;

public partial class SelectedCoursePage : ContentPage
{
	public SelectedCoursePage()
	{
		InitializeComponent();
	}

	private async void OnBackClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}