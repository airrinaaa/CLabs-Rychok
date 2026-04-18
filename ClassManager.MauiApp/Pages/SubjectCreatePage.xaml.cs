using ClassManager.MauiApp.ViewModels;

namespace ClassManager.MauiApp.Pages;

public partial class SubjectCreatePage : ContentPage
{
    public SubjectCreatePage(SubjectCreateViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}