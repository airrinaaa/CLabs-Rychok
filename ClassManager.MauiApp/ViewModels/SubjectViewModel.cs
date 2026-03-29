using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using ClassManager.DTOModels.Subjects;
using ClassManager.Services;
using ClassManager.MauiApp.Pages;

namespace ClassManager.MauiApp.ViewModels
{
    //ViewModel for the main subjects page
    public partial class SubjectViewModel : ObservableObject
    {
        private readonly ISubjectService _subjectService;
        [ObservableProperty]
        private ObservableCollection<SubjectListDTO> _subjects;
        /// <summary>
        /// initializes the ViewModel and loads the initial data
        /// </summary>
        /// <param name="subjectService"></param>
        public SubjectViewModel(ISubjectService subjectService)
        {
            _subjectService = subjectService;
            Subjects = new ObservableCollection<SubjectListDTO>(_subjectService.GetAllSubjects());
        }
        /// <summary>
        /// navigates to the SubjectDetailsPage and passes the selected subject ID
        /// </summary>
        /// <param name="subjectId"></param>
        [RelayCommand]
        private void LoadSubject(Guid subjectId)
        {
            Shell.Current.GoToAsync($"{nameof(SubjectDetailsPage)}", new Dictionary<string, object> 
            { 
                { "SubjectId", subjectId } 
            });
        }
    }
    
}