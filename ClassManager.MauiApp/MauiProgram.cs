using ClassManager.MauiApp.Pages;
using ClassManager.MauiApp.ViewModels;
using ClassManager.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ClassManager.MauiApp;

public static class MauiProgram
{
    public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
    {
        var builder = Microsoft.Maui.Hosting.MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<AppShell>();

        builder.Services.AddSingleton<SubjectsPage>();
        builder.Services.AddTransient<SubjectDetailsPage>();
        builder.Services.AddTransient<LessonDetailsPage>();
        builder.Services.AddTransient<SubjectCreatePage>();
        builder.Services.AddTransient<SubjectEditPage>();
        builder.Services.AddTransient<LessonCreatePage>();
        builder.Services.AddTransient<LessonEditPage>();
        builder.Services.AddSingleton<SubjectViewModel>();
        builder.Services.AddTransient<SubjectDetailsViewModel>();
        builder.Services.AddTransient<LessonDetailsViewModel>();
        builder.Services.AddTransient<SubjectCreateViewModel>();
        builder.Services.AddTransient<SubjectEditViewModel>();
        builder.Services.AddTransient<LessonCreateViewModel>();
        builder.Services.AddTransient<LessonEditViewModel>();

        builder.Services.AddClassManagerDependencies();

        return builder.Build();
    }
}