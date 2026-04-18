using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassManager.Common.Enums;
using ClassManager.DTOModels.Subjects;
using ClassManager.Services;
using ClassManager.MauiApp.Pages;

namespace ClassManager.MauiApp.ViewModels
{
    //ViewModel for the main subjects page
    public partial class SubjectViewModel : BaseViewModel
    {
        private readonly ISubjectService _subjectService;
        private readonly List<SubjectListDTO> _allSubjects = new();

        [ObservableProperty]
        private ObservableCollection<SubjectListDTO> _subjects = new();

        [ObservableProperty]
        private string _searchText = string.Empty;

        [ObservableProperty]
        private string _selectedSphereFilter = "All";

        [ObservableProperty]
        private string _selectedSortOption = "Name (A-Z)";

        public List<string> SphereFilters { get; }
        public List<string> SortOptions { get; }

        /// <summary>
        /// initializes the ViewModel and loads the initial data
        /// </summary>
        /// <param name="subjectService"></param>
        public SubjectViewModel(ISubjectService subjectService)
        {
            _subjectService = subjectService;

            SphereFilters = new List<string> { "All" };
            SphereFilters.AddRange(Enum.GetNames(typeof(SubjectSphere)));

            SortOptions = new List<string>
            {
                "Name (A-Z)",
                "Name (Z-A)",
                "Credits (Low to High)",
                "Credits (High to Low)"
            };
        }

        partial void OnSearchTextChanged(string value)
        {
            ApplyFiltersAndSorting();
        }

        partial void OnSelectedSphereFilterChanged(string value)
        {
            ApplyFiltersAndSorting();
        }

        partial void OnSelectedSortOptionChanged(string value)
        {
            ApplyFiltersAndSorting();
        }

        [RelayCommand]
        public async Task RefreshData()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                _allSubjects.Clear();

                await foreach (var subject in _subjectService.GetAllSubjectsAsync())
                {
                    _allSubjects.Add(subject);
                }

                ApplyFiltersAndSorting();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load subjects: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ApplyFiltersAndSorting()
        {
            IEnumerable<SubjectListDTO> filteredSubjects = _allSubjects;

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredSubjects = filteredSubjects.Where(subject =>
                    subject.Name.Contains(SearchText.Trim(), StringComparison.OrdinalIgnoreCase));
            }

            if (SelectedSphereFilter != "All" &&
                Enum.TryParse<SubjectSphere>(SelectedSphereFilter, out var selectedSphere))
            {
                filteredSubjects = filteredSubjects.Where(subject => subject.Sphere == selectedSphere);
            }

            filteredSubjects = SelectedSortOption switch
            {
                "Name (Z-A)" => filteredSubjects.OrderByDescending(subject => subject.Name),
                "Credits (Low to High)" => filteredSubjects.OrderBy(subject => subject.Credits),
                "Credits (High to Low)" => filteredSubjects.OrderByDescending(subject => subject.Credits),
                _ => filteredSubjects.OrderBy(subject => subject.Name)
            };

            Subjects = new ObservableCollection<SubjectListDTO>(filteredSubjects);
        }

        /// <summary>
        /// navigates to the SubjectDetailsPage and passes the selected subject ID
        /// </summary>
        /// <param name="subjectId"></param>
        [RelayCommand]
        private async Task LoadSubject(Guid subjectId)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await Shell.Current.GoToAsync(nameof(SubjectDetailsPage), new Dictionary<string, object>
                {
                    { "SubjectId", subjectId }
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to open subject details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task AddSubject()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await Shell.Current.GoToAsync(nameof(SubjectCreatePage));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to open create subject page: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task EditSubject(Guid subjectId)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await Shell.Current.GoToAsync(nameof(SubjectEditPage), new Dictionary<string, object>
                {
                    { "SubjectId", subjectId }
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

        [RelayCommand]
        private async Task DeleteSubject(SubjectListDTO subject)
        {
            if (subject == null || IsBusy)
                return;

            IsBusy = true;

            try
            {
                var confirmed = await Shell.Current.DisplayAlertAsync(
                    "Confirm",
                    "Are you sure you want to delete this subject?",
                    "Yes",
                    "No");

                if (!confirmed)
                    return;

                await _subjectService.DeleteSubjectAsync(subject.Id);

                var subjectToRemove = _allSubjects.FirstOrDefault(s => s.Id == subject.Id);
                if (subjectToRemove != null)
                {
                    _allSubjects.Remove(subjectToRemove);
                }

                ApplyFiltersAndSorting();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to delete subject: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}