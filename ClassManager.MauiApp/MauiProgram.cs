using ClassManager.MauiApp.Pages;
using ClassManager.Services;
using CommunityToolkit.Maui;
using ClassManager.Storage;
using ClassManager.Repositories; 
using Microsoft.Extensions.Logging;
using ClassManager.MauiApp.ViewModels;

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

        builder.Services.AddSingleton<IStorageContext, InMemoryStorageContext>();

        builder.Services.AddSingleton<ISubjectRepository, SubjectRepository>();
        builder.Services.AddSingleton<ILessonRepository, LessonRepository>();

        builder.Services.AddSingleton<ISubjectService, SubjectService>();
        builder.Services.AddTransient<ILessonService, LessonService>();
        
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<SubjectsPage>(); 
        builder.Services.AddTransient<SubjectDetailsPage>();
        builder.Services.AddTransient<LessonDetailsPage>();

        builder.Services.AddSingleton<SubjectViewModel>();
        builder.Services.AddTransient<SubjectDetailsViewModel>();
        builder.Services.AddTransient<LessonDetailsViewModel>();
        

        return builder.Build();
    }
}