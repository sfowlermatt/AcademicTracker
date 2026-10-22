using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AcademicTracker.Models;
using AcademicTracker.Services;
using AcademicTracker.Views;


namespace AcademicTracker.ViewModels;

[QueryProperty(nameof(TermId), "termId")]
public partial class SelectedTermViewModel : ObservableObject
{
	private readonly ITerms _termService;
	private readonly ICourses _courseService;

	public SelectedTermViewModel(ITerms termService, ICourses courseService)
	{
		_termService = termService;
		_courseService = courseService;
	}

	public ObservableCollection<Course> Courses { get; } = new();

    [ObservableProperty]
	private string? termId;

	[ObservableProperty]
    private Term? term;

	[ObservableProperty]
	private string dateRange = string.Empty;

	[ObservableProperty]
    private bool isBusy;

	partial void OnTermIdChanged(string? value)
	{
		LoadTermDetailsCommand.Execute(null);
    }

	[RelayCommand]
    private async Task LoadTermDetailsAsync()
    {
        if (IsBusy || !int.TryParse(TermId, out var id))
            return;

        try
        {
            IsBusy = true;
            var loadedTerm = await _termService.GetTermByIdAsync(id);
            if (loadedTerm is null)
                return;
            Term = loadedTerm;
            DateRange = $"{loadedTerm.StartDate:MM/dd/yyyy} - {loadedTerm.EndDate:MM/dd/yyyy}";

            var courses = await _courseService.GetCoursesByTermIdAsync(id);
            Courses.Clear();
            foreach (var course in courses)
            {
                Courses.Add(course);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading term details: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task OpenCourseAsync(Course course)
    {
        if (course == null) return;
        await Shell.Current.GoToAsync($"{nameof(SelectedCoursePage)}?courseId={course.Id}");
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task EditTerm()
    {
        if (Term == null) return;
        await Shell.Current.GoToAsync($"{nameof(TermEditPage)}?termId={Term.Id}");
    }
}