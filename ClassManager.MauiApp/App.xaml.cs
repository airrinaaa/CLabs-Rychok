namespace ClassManager.MauiApp;

public partial class App : Application
{
    private readonly AppShell _shell;

    public static IServiceProvider Services { get; private set; } = null!;

    public App(AppShell shell, IServiceProvider services)
    {
        InitializeComponent();
        _shell = shell;
        Services = services;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(_shell);
    }
}