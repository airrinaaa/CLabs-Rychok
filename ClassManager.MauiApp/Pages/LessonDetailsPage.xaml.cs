using Microsoft.Maui.Controls;
using ClassManager.MauiApp.ViewModels;

namespace ClassManager.MauiApp.Pages;

public partial class LessonDetailsPage : ContentPage
{
    private readonly LessonDetailsViewModel _viewModel;
    public LessonDetailsPage(LessonDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.RefreshData();
    }
}