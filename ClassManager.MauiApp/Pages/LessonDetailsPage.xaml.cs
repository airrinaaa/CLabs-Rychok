using Microsoft.Maui.Controls;
using ClassManager.MauiApp.ViewModels; 

namespace ClassManager.MauiApp.Pages;

public partial class LessonDetailsPage : ContentPage
{
    public LessonDetailsPage(LessonDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; 
    }
}