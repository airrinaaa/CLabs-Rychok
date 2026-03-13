using System.Collections.ObjectModel;
using ClassManager.Services;
using ClassManager.UIModels;
using Microsoft.Extensions.DependencyInjection;

namespace ClassManager.MauiApp.Pages;

public partial class SubjectsPage : ContentPage
{
    private readonly IStorageService _storage;

    public ObservableCollection<SubjectUIModel> Subjects { get; set; }

    //creating subjects page and loads all subjects from the storage
    public SubjectsPage()
    {
        InitializeComponent();
        _storage = App.Services.GetRequiredService<IStorageService>();
        Subjects = new ObservableCollection<SubjectUIModel>();

        foreach (var subject in _storage.GetAllSubjects())
        {
            Subjects.Add(new SubjectUIModel(subject));
        }

        BindingContext = this;
    }

    //opens subject details page for the selected subject
    public async void SubjectSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        var subject = (SubjectUIModel)e.CurrentSelection[0];
        await Shell.Current.GoToAsync(nameof(SubjectDetailsPage), new Dictionary<string, object>
        {
            { "SelectedSubject", subject }
        });

        if (sender is CollectionView collectionView)
            collectionView.SelectedItem = null;
    }
    //refreshes subjects list when user returns back to the page
    protected override void OnAppearing()
    {
        base.OnAppearing();

        SubjectsCollectionView.ItemsSource = null;
        SubjectsCollectionView.ItemsSource = Subjects;
    }

}
