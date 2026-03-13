using ClassManager.Services;
using ClassManager.UIModels;


namespace ClassManager.MauiApp.Pages;

[QueryProperty(nameof(CurrentSubject), "SelectedSubject")]
public partial class SubjectDetailsPage : ContentPage
{
    private readonly IStorageService _storage;
    private SubjectUIModel? _currentSubject;

    public SubjectUIModel? CurrentSubject
    {
        get => _currentSubject;
        set
        {
            _currentSubject = value;
            if (_currentSubject != null)
            {
                _currentSubject.LoadLessons(_storage);
                BindingContext = CurrentSubject;
            }
        }
    }
    //creates subject details page
    public SubjectDetailsPage()
    {
        InitializeComponent();
        _storage = App.Services.GetRequiredService<IStorageService>();
    }

    //opens lesson details page for the selected lesson
    private async void LessonsCollectionView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        if (e.CurrentSelection[0] is LessonUIModel selectedLesson)
        {
            await Shell.Current.GoToAsync(nameof(LessonDetailsPage), new Dictionary<string, object>
            {
                { "SelectedLesson", selectedLesson }
            });

            if (sender is CollectionView collectionView)
                collectionView.SelectedItem = null;
        }
    }
}
