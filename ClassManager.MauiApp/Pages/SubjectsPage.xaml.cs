using ClassManager.MauiApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ClassManager.MauiApp.Pages;

public partial class SubjectsPage : ContentPage
{
    private readonly SubjectViewModel _viewModel;

    public SubjectsPage(SubjectViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.RefreshData();
    }
}