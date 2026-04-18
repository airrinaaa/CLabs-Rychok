using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassManager.Common.Enums;
using ClassManager.DTOModels.Lessons;
using ClassManager.Services;

namespace ClassManager.MauiApp.ViewModels
{
    public partial class LessonCreateViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ILessonService _lessonService;
        private Guid _subjectId;

        [ObservableProperty]
        private string _topic = string.Empty;

        [ObservableProperty]
        private LessonType? _type;

        [ObservableProperty]
        private DateTime _date = DateTime.Today;

        [ObservableProperty]
        private TimeSpan _startTime = new TimeSpan(9, 0, 0);

        [ObservableProperty]
        private TimeSpan _endTime = new TimeSpan(10, 30, 0);

        public Array LessonTypes => Enum.GetValues(typeof(LessonType));

        public LessonCreateViewModel(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _subjectId = (Guid)query["SubjectId"];
        }

        [RelayCommand]
        public async Task CreateLesson()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var newLesson = new LessonCreateDTO(_subjectId, Date, StartTime, EndTime, Topic, Type);
                await _lessonService.CreateLessonAsync(newLesson);
                await Shell.Current.DisplayAlertAsync("Success", "Lesson created successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to create lesson: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task Back()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate back: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}