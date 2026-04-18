using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassManager.Common.Enums;
using ClassManager.DTOModels.Lessons;
using ClassManager.DTOModels.Subjects;
using ClassManager.MauiApp.Pages;
using ClassManager.Services;

namespace ClassManager.MauiApp.ViewModels
{
    //ViewModel for the Subject Details page
    public partial class SubjectDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ISubjectService _subjectService;
        private readonly ILessonService _lessonService;

        private Guid _subjectId;
        private readonly List<LessonListDTO> _allLessons = new();

        //currently selected subject
        [ObservableProperty]
        private SubjectDetailsDTO? _currentSubject;

        //list of lessons associated with the current subject
        [ObservableProperty]
        private ObservableCollection<LessonListDTO> _lessons = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string _selectedTypeFilter = "All";

        [ObservableProperty]
        private string _selectedSortOption = "Date (Newest first)";

        public List<string> TypeFilters { get; }
        public List<string> SortOptions { get; }

        /// <summary>
        /// initializes the ViewModel
        /// </summary>
        /// <param name="subjectService"></param>
        /// <param name="lessonService"></param>
        public SubjectDetailsViewModel(ISubjectService subjectService, ILessonService lessonService)
        {
            _subjectService = subjectService;
            _lessonService = lessonService;

            TypeFilters = new List<string> { "All" };
            TypeFilters.AddRange(Enum.GetNames(typeof(LessonType)));

            SortOptions = new List<string>
            {
                "Date (Newest first)",
                "Date (Oldest first)",
                "Topic (A-Z)",
                "Topic (Z-A)"
            };
        }

        partial void OnSearchTextChanged(string value)
        {
            ApplyFiltersAndSorting();
        }

        partial void OnSelectedTypeFilterChanged(string value)
        {
            ApplyFiltersAndSorting();
        }

        partial void OnSelectedSortOptionChanged(string value)
        {
            ApplyFiltersAndSorting();
        }

        /// <summary>
        /// loads subject data based on the ID passed during navigation
        /// </summary>
        /// <param name="query"></param>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _subjectId = (Guid)query["SubjectId"];
        }

        [RelayCommand]
        public async Task RefreshData()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var subjectTask = _subjectService.GetSubjectAsync(_subjectId);
                var lessonsTask = _lessonService.GetLessonsBySubjectAsync(_subjectId);

                CurrentSubject = await subjectTask ?? throw new Exception("Subject does not exist.");

                _allLessons.Clear();
                _allLessons.AddRange(await lessonsTask);

                ApplyFiltersAndSorting();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load subject details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ApplyFiltersAndSorting()
        {
            IEnumerable<LessonListDTO> filteredLessons = _allLessons;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredLessons = filteredLessons.Where(lesson =>
                    lesson.Topic.Contains(SearchText.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (SelectedTypeFilter != "All" &&
                Enum.TryParse<LessonType>(SelectedTypeFilter, out var selectedType))
            {
                filteredLessons = filteredLessons.Where(lesson => lesson.Type == selectedType);
            }

            filteredLessons = SelectedSortOption switch
            {
                "Date (Oldest first)" => filteredLessons.OrderBy(lesson => lesson.Date),
                "Topic (A-Z)" => filteredLessons.OrderBy(lesson => lesson.Topic),
                "Topic (Z-A)" => filteredLessons.OrderByDescending(lesson => lesson.Topic),
                _ => filteredLessons.OrderByDescending(lesson => lesson.Date)
            };

            Lessons = new ObservableCollection<LessonListDTO>(filteredLessons);
        }

        [RelayCommand]
        private async Task EditSubject()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await Shell.Current.GoToAsync(nameof(SubjectEditPage), new Dictionary<string, object>
                {
                    { "SubjectId", _subjectId }
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to open edit subject page: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// navigates to the Lesson Details page with the selected lesson ID
        /// </summary>
        /// <param name="lessonId"></param>
        [RelayCommand]
        private async Task LoadLesson(Guid lessonId)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await Shell.Current.GoToAsync(nameof(LessonDetailsPage), new Dictionary<string, object>
                {
                    { "LessonId", lessonId }
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to open lesson details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task AddLesson()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await Shell.Current.GoToAsync(nameof(LessonCreatePage), new Dictionary<string, object>
                {
                    { "SubjectId", _subjectId }
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to navigate to lesson create page: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task EditLesson(Guid lessonId)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await Shell.Current.GoToAsync(nameof(LessonEditPage), new Dictionary<string, object>
                {
                    { "LessonId", lessonId }
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to open lesson edit page: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteLesson(LessonListDTO lesson)
        {
            if (lesson == null || IsBusy)
                return;

            var shouldRefresh = false;

            IsBusy = true;

            try
            {
                var confirmed = await Shell.Current.DisplayAlertAsync(
                    "Confirm",
                    "Are you sure you want to delete this lesson?",
                    "Yes",
                    "No");

                if (!confirmed)
                    return;

                await _lessonService.DeleteLessonAsync(lesson.Id);
                shouldRefresh = true;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to delete lesson: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }

            if (shouldRefresh)
            {
                await RefreshData();
            }
        }
    }
}