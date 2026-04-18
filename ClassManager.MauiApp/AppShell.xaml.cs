using ClassManager.MauiApp.Pages;

namespace ClassManager.MauiApp;

public partial class AppShell : Shell
{
    public AppShell()
{
    InitializeComponent();

    Routing.RegisterRoute(nameof(SubjectDetailsPage), typeof(SubjectDetailsPage));
    Routing.RegisterRoute(nameof(LessonDetailsPage), typeof(Pages.LessonDetailsPage));
    Routing.RegisterRoute(nameof(LessonCreatePage), typeof(LessonCreatePage));
    Routing.RegisterRoute(nameof(SubjectCreatePage), typeof(SubjectCreatePage));
    Routing.RegisterRoute(nameof(SubjectEditPage), typeof(SubjectEditPage));
    Routing.RegisterRoute(nameof(LessonEditPage), typeof(LessonEditPage));
}
}