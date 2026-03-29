namespace ClassManager.MauiApp;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    public App(AppShell shell, IServiceProvider services)
    {
       MainPage = new AppShell();
    }
}
