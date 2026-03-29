using ClassManager.MauiApp.Pages;

namespace ClassManager.MauiApp;

public partial class AppShell : Shell
{
    public AppShell()
{
    InitializeComponent();

    Routing.RegisterRoute(nameof(SubjectDetailsPage), typeof(SubjectDetailsPage));
    Routing.RegisterRoute(nameof(LessonDetailsPage), typeof(Pages.LessonDetailsPage));
}
}