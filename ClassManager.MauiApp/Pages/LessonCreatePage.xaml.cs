
using ClassManager.Common.Enums; 
namespace ClassManager.MauiApp.Pages;

public partial class LessonCreatePage : ContentPage
{
    public LessonCreatePage()
    {
        InitializeComponent();
        pLessonType.ItemsSource = Enum.GetValues<LessonType>();
    }

    
    private void CreateClicked(object sender, EventArgs e){
        if (pSubject.SelectedItem == null){
        DisplayAlert("Incomplete data!", "Subject must be selected", "OK");
        return;
    }

    if (string.IsNullOrWhiteSpace(eTopic.Text)){
        DisplayAlert("Incomplete data!", "Topic can't be empty", "OK");
        return;
    }

    if (pLessonType.SelectedItem == null){
        DisplayAlert("Incomplete data!", "Lesson Type must be selected", "OK");
        return;
    }

    if (tpStartTime.Time >= tpEndTime.Time){
        DisplayAlert("Invalid data!", "Start time must be before End time", "OK");
        return;
    }

    if (dpDate.Date < DateTime.Today){
        DisplayAlert("Invalid data!", "The lesson date cannot be in the past", "OK");
        return;
    }
    var selectedSubject = (ClassManager.DBModels.SubjectDBModel)pSubject.SelectedItem;
    var selectedType = (ClassManager.Common.Enums.LessonType)pLessonType.SelectedItem;
    var newLesson = new ClassManager.DBModels.LessonDBModel(
        selectedSubject.Id,
        dpDate.Date ?? DateTime.Today,
        tpStartTime.Time ?? TimeSpan.Zero,
        tpEndTime.Time ?? TimeSpan.Zero,
        eTopic.Text,
        selectedType
    );

    DisplayAlert("Lesson Created!", $"Lesson '{newLesson.Topic}' was created successfully for {newLesson.Date.ToShortDateString()}", "OK");
    }

    private void BackClicked(object sender, EventArgs e)
    {

        Shell.Current.GoToAsync(".."); 
    }
}