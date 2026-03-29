using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ClassManager.DTOModels.Subjects;
using ClassManager.DTOModels.Lessons;
using ClassManager.Services;
using ClassManager.MauiApp.Pages;
using CommunityToolkit.Mvvm.Input;
namespace ClassManager.MauiApp.ViewModels
{
    //ViewModel for the Subject Details page
    public partial class SubjectDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ISubjectService _subjectService;
        private readonly ILessonService _lessonService;

        //currently selected subject
        [ObservableProperty]
        private SubjectDetailsDTO _currentSubject;

        //list of lessons associated with the current subject
        [ObservableProperty]
        private ObservableCollection<LessonListDTO> _lessons;
        /// <summary>
        /// initializes the ViewModel
        /// </summary>
        /// <param name="subjectService"></param>
        /// <param name="lessonService"></param>
        public SubjectDetailsViewModel(ISubjectService subjectService, ILessonService lessonService)
        {
            _subjectService = subjectService;
            _lessonService = lessonService;
        }
        /// <summary>
        /// loads subject data based on the ID passed during navigation
        /// </summary>
        /// <param name="query"></param>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var subjectId = (Guid)query["SubjectId"];

            CurrentSubject = _subjectService.GetSubject(subjectId);
            Lessons = new ObservableCollection<LessonListDTO>(_lessonService.GetLessonsBySubject(subjectId));
        
            OnPropertyChanged(nameof(Lessons));
        }

        /// <summary>
        /// navigates to the Lesson Details page with the selected lesson ID
        /// </summary>
        /// <param name="lessonId"></param>
        [RelayCommand]
        private void LoadLesson(Guid lessonId) 
        {
            Shell.Current.GoToAsync($"{nameof(LessonDetailsPage)}", new Dictionary<string, object> 
            { 
                { "LessonId", lessonId } 
            });
        }
    }
}