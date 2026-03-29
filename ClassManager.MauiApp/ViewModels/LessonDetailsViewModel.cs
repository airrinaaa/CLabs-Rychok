using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using ClassManager.DTOModels.Lessons;
using ClassManager.Services;
using Microsoft.Maui.Controls;

namespace ClassManager.MauiApp.ViewModels
{
    //ViewModel for displaying details of a specific lesson
    public partial class LessonDetailsViewModel : ObservableObject, IQueryAttributable
    {
        private readonly ILessonService _lessonService;
        //currently displayed lesson
        [ObservableProperty]
        private LessonDetailsDTO _currentLesson;

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
                var lessonId = (Guid)query["LessonId"];
                CurrentLesson = _lessonService.GetLesson(lessonId);
            }
        }
    }
}