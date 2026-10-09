namespace AcademicTracker.Views;

[QueryProperty(nameof(TermId), "termId")]
public partial class SelectedTermPage : ContentPage
{
    private string _termId;

    public string TermId
    {
        get => _termId;
        set
        {
            _termId = value;
            TermIdLabel.Text = $"Term ID: {value}";
        }
    }

    public SelectedTermPage()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object sender, EventArgs e)
        => await Shell.Current.GoToAsync("..");
}