using ClassManager.MauiApp.ViewModels;

namespace ClassManager.MauiApp.Pages;

public partial class LessonEditPage : ContentPage
{
    private readonly LessonEditViewModel _viewModel;

    public LessonEditPage(LessonEditViewModel vm)
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