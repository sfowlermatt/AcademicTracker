namespace AcademicTracker.Views;

public partial class SelectedTerm : ContentPage
{
    [QueryProperty(nameof(TermId), "termId")]

    public partial class SelectedTerm : ContentPage
    {
        private int _termId;
        public int TermId
        {
            get => _termId;
            set
            {
                _termId = value;
                TermIdLabel.Text = $"Selected Term ID: {_termId}";
            }
        }
    }
        
    public SelectedTerm()
	    {
		    InitializeComponent();
	    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}