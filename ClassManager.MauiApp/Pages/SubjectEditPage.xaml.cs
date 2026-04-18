using ClassManager.MauiApp.ViewModels;

namespace ClassManager.MauiApp.Pages;

public partial class SubjectEditPage : ContentPage
{
    private readonly SubjectEditViewModel _viewModel;

    public SubjectEditPage(SubjectEditViewModel vm)
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