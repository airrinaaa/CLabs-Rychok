using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassManager.Common.Enums;
using ClassManager.DTOModels.Lessons;
using ClassManager.Services;

namespace ClassManager.MauiApp.ViewModels
{
    public partial class LessonEditViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ILessonService _lessonService;

        private Guid _lessonId;

        [ObservableProperty]
        private string _topic = string.Empty;

        [ObservableProperty]
        private DateTime _date = DateTime.Today;

        [ObservableProperty]
        private TimeSpan _startTime;

        [ObservableProperty]
        private TimeSpan _endTime;

        [ObservableProperty]
        private LessonType? _type;

        public Array LessonTypes => Enum.GetValues(typeof(LessonType));

        public LessonEditViewModel(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _lessonId = (Guid)query["LessonId"];
        }

        internal async Task RefreshData()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var lesson = await _lessonService.GetLessonAsync(_lessonId);

                if (lesson == null)
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Lesson was not found.", "OK");
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                Topic = lesson.Topic;
                Date = lesson.Date;
                StartTime = lesson.StartTime;
                EndTime = lesson.EndTime;
                Type = lesson.Type;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load lesson for edit: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task SaveChanges()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var oldLesson = await _lessonService.GetLessonAsync(_lessonId);

                if (oldLesson == null)
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Lesson was not found.", "OK");
                    return;
                }

                var updatedLesson = new LessonUpdateDTO(
                    _lessonId,
                    oldLesson.SubjectId,
                    Date,
                    StartTime,
                    EndTime,
                    Topic,
                    Type);

                await _lessonService.UpdateLessonAsync(updatedLesson);

                await Shell.Current.DisplayAlertAsync("Success", "Lesson updated successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to update lesson: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task Back()
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