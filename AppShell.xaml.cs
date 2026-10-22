using AcademicTracker.Views;

namespace AcademicTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SelectedTermPage), typeof(SelectedTermPage));
            Routing.RegisterRoute(nameof(SelectedCoursePage), typeof(SelectedCoursePage));
            Routing.RegisterRoute(nameof(TermEditPage), typeof(TermEditPage));
        }
    }
}
