using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace ClassManager.MauiApp;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate 
{
    protected override Microsoft.Maui.Hosting.MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}