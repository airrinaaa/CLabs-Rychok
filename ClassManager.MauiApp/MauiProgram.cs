using ClassManager.MauiApp.Pages;
using ClassManager.Services;
using Microsoft.Extensions.Logging;

namespace ClassManager.MauiApp;

public static class MauiProgram
{
    public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
    {
        var builder = Microsoft.Maui.Hosting.MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IStorageService, StorageService>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddTransient<SubjectsPage>();
        builder.Services.AddTransient<SubjectDetailsPage>();
        builder.Services.AddTransient<LessonDetailsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
