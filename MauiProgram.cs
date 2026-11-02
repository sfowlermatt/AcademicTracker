using Microsoft.Extensions.Logging;
using AcademicTracker.Services;
using AcademicTracker.Views;
using AcademicTracker.ViewModels;
using Plugin.LocalNotification;

namespace AcademicTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<INotifications, NotificationService>();
            builder.Services.AddSingleton<ITerms, TermService>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<SelectedTermPage>();
            builder.Services.AddSingleton<ICourses, CourseService>();
            builder.Services.AddTransient<SelectedTermViewModel>();
            builder.Services.AddTransient<SelectedCourseViewModel>();
            builder.Services.AddSingleton<IAssessments, AssessmentService>();
            builder.Services.AddTransient<TermEditViewModel>();
            builder.Services.AddTransient<TermEditPage>();
            builder.Services.AddTransient<CourseEditViewModel>();
            builder.Services.AddTransient<CourseEditView>();
            builder.Services.AddTransient<AssessmentEditViewModel>();
            builder.Services.AddTransient<AssessmentEditView>();
            //builder.Services.AddTransient<AddCoursePage>();

            return builder.Build();
        }
    }
}
