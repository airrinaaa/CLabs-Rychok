using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassManager.Common.Enums;
using ClassManager.DTOModels.Subjects;
using ClassManager.Services;

namespace ClassManager.MauiApp.ViewModels
{
    public partial class SubjectEditViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ISubjectService _subjectService;
        private Guid _subjectId;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _credits = string.Empty;

        [ObservableProperty]
        private SubjectSphere? _sphere;

        public Array SubjectSpheres => Enum.GetValues(typeof(SubjectSphere));

        public SubjectEditViewModel(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _subjectId = (Guid)query["SubjectId"];
        }

        internal async Task RefreshData()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var subject = await _subjectService.GetSubjectAsync(_subjectId);

                if (subject == null)
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Subject was not found.", "OK");
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                Name = subject.Name;
                Credits = subject.Credits.ToString();
                Sphere = subject.Sphere;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load subject for edit: {ex.Message}", "OK");
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
                if (!double.TryParse(Credits, out var parsedCredits))
                {
                    await Shell.Current.DisplayAlertAsync("Error", "Credits must be a valid number.", "OK");
                    return;
                }

                var updatedSubject = new SubjectUpdateDTO(_subjectId, Name, parsedCredits, Sphere);

                await _subjectService.UpdateSubjectAsync(updatedSubject);

                await Shell.Current.DisplayAlertAsync("Success", "Subject updated successfully!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to update subject: {ex.Message}", "OK");
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