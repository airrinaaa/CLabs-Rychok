using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassManager.Common.Enums;
using ClassManager.DTOModels.Subjects;
using ClassManager.Services;

namespace ClassManager.MauiApp.ViewModels
{
    public partial class SubjectCreateViewModel : BaseViewModel
    {
        private readonly ISubjectService _subjectService;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _credits = string.Empty;

        [ObservableProperty]
        private SubjectSphere? _sphere;

        public Array SubjectSpheres => Enum.GetValues(typeof(SubjectSphere));

        public SubjectCreateViewModel(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [RelayCommand]
        private async Task CreateSubject()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (!double.TryParse(Credits, out var parsedCredits))
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Credits must be a valid number.", "OK");
                    return;
                }

                var newSubject = new SubjectCreateDTO(Name, parsedCredits, Sphere);

                await _subjectService.CreateSubjectAsync(newSubject);

                await Shell.Current.DisplayAlertAsync("Success", "Subject created successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to create subject: {ex.Message}", "OK");
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