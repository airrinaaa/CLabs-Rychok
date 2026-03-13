using ClassManager.UIModels;

namespace ClassManager.MauiApp.Pages;

[QueryProperty(nameof(CurrentLesson), "SelectedLesson")]
public partial class LessonDetailsPage : ContentPage
{
    private LessonUIModel? _currentLesson;

    public LessonUIModel? CurrentLesson
    {
        get => _currentLesson;
        set
        {
            _currentLesson = value;
            if (_currentLesson != null)
            {
                UpdateUI();
            }
        }
    }
    //creates lesson details page
    public LessonDetailsPage()
    {
        InitializeComponent();
    }

    //updates all ui elements with lesson data
    private void UpdateUI()
    {
        if (_currentLesson == null)
            return;

        LessonTopicLabel.Text = _currentLesson.Topic;
        LessonDateLabel.Text = _currentLesson.Date.ToString("dd MMMM yyyy");
        LessonTypeLabel.Text = _currentLesson.Type.ToString();
        LessonStartTimeLabel.Text = _currentLesson.StartTime.ToString(@"hh\:mm");
        LessonEndTimeLabel.Text = _currentLesson.EndTime.ToString(@"hh\:mm");

        var duration = _currentLesson.LessonDuration;
        LessonDurationLabel.Text = $"{(int)duration.TotalMinutes} minutes";

        LessonInfoLabel.Text =
            $"This {_currentLesson.Type.ToString().ToLower()} on {_currentLesson.Topic} " +
            $"is scheduled for {_currentLesson.Date:dddd, MMMM d, yyyy}. " +
            $"The session starts at {_currentLesson.StartTime:hh\\:mm} and ends at {_currentLesson.EndTime:hh\\:mm}.";

        Title = _currentLesson.Topic;
    }
}
