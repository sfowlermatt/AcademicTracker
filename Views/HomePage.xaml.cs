using AcademicTracker.ViewModels;
using AcademicTracker.Models;


namespace AcademicTracker.Views;

public partial class HomePage : ContentPage
{
	private readonly HomeViewModel _viewModel;
	public HomePage(HomeViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.Terms.Count == 0)
            _viewModel.LoadTermCommand.Execute(null);
    }

    public async void OnTermTapped(object sender, TappedEventArgs e)
    {
        if (sender is not Element { BindingContext: Term term })
            return;
        
        await Shell.Current.GoToAsync($"{nameof(SelectedTermPage)}?termId={term.Id}");
        
    }
}