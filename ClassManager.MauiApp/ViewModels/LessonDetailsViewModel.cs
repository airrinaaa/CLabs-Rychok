using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClassManager.DTOModels.Lessons;
using ClassManager.MauiApp.Pages;
using ClassManager.Services;
using Microsoft.Maui.Controls;

namespace ClassManager.MauiApp.ViewModels
{
    //ViewModel for displaying details of a specific lesson
    public partial class LessonDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ILessonService _lessonService;
        private Guid _lessonId;

        //currently displayed lesson
        [ObservableProperty]
        private LessonDetailsDTO? _currentLesson;

        /// <summary>
        /// initializes the ViewModel with the required lesson service
        /// </summary>
        /// <param name="lessonService"></param>
        public LessonDetailsViewModel(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        /// <summary>
        /// loads lesson details using the ID passed during navigation
        /// </summary>
        /// <param name="query"></param>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("LessonId"))
            {
                _lessonId = (Guid)query["LessonId"];
            }
        }

        [RelayCommand]
        public async Task RefreshData()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                CurrentLesson = await _lessonService.GetLessonAsync(_lessonId);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error", $"Failed to load lesson details: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task EditLesson()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await Shell.Current.GoToAsync(nameof(LessonEditPage), new Dictionary<string, object>
                {
                    { "LessonId", _lessonId }
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
    }
}