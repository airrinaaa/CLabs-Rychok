using ClassManager.MauiApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ClassManager.MauiApp.Pages;

public partial class SubjectsPage : ContentPage
{
    public SubjectsPage(SubjectViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}