using ClassManager.MauiApp.ViewModels;
using ClassManager.Services;
using ClassManager.UIModels;


namespace ClassManager.MauiApp.Pages;

public partial class SubjectDetailsPage : ContentPage
{
    public SubjectDetailsPage(SubjectDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
