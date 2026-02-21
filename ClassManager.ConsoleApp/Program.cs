using System;
using System.Collections.Generic;
using System.Linq;
using ClassManager.Services;
using ClassManager.UIModels;
// main class for the console aaplication
namespace ClassManager.ConsoleApp
{
    internal class Program
    {
        // enum that holds the states of our application menu
        enum AppState
        {
            Default = 0,
            SubjectDetails = 1,
            End = 2,
            Exit = 100,
        }
        private static AppState _appState = AppState.Default;
        private static StorageService _storageService;
        private static List<SubjectUIModel> _subjects;

        // main menu
        static void Main(string[] args)
        {
            Console.WriteLine("Hello and welcome to the Class Manager Console App!");
            
            _storageService = new StorageService();
            string? command = null;

            while (_appState != AppState.Exit)
            {
                switch (_appState)
                {
                    case AppState.SubjectDetails:
                        SubjectDetailsState(command);
                        break;
                    case AppState.Default:
                        DefaultState();
                        break;
                }

                command = Console.ReadLine();
                UpdateState(command);
            }
        }

        /// <summary>
        /// this method changes the application state based on user`s input
        /// </summary>
        /// <param name="command"></param>
        private static void UpdateState(string? command)
        {
            switch (command?.Trim().ToLower()) 
            {
                case "back":
                    _appState = AppState.Default;
                    break;
                case "exit":
                    _appState = AppState.Exit;
                    Console.WriteLine("Thank you and see you later!");
                    break;
                default:
                    switch (_appState)
                    {
                        case AppState.Default:
                            _appState = AppState.SubjectDetails;
                            break;
                        case AppState.End:
                            Console.WriteLine("Unknown command. Please try again.");
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// this method shows the default main menu with the list of all subjects
        /// </summary>
        private static void DefaultState()
        {
            Console.WriteLine("\nHere is the list of all Subjects:");
            LoadSubjects();
            
            foreach (var subject in _subjects)
            {
                Console.WriteLine(subject); 
            }
            
            Console.WriteLine("\nType the name of Subject to see its Lessons. Or type exit to close application.");
        }

        /// <summary>
        /// this method loads subjects from the storage service
        /// </summary>
        private static void LoadSubjects()
        {
            if (_subjects != null)
                return;

            _subjects = new List<SubjectUIModel>();
            
            foreach (var subject in _storageService.GetAllSubjects())
            {
                var subjectUIModel = new SubjectUIModel(subject);
                _subjects.Add(subjectUIModel);
            }
        }
        
        /// <summary>
        /// this method shows details about one subject and all its lessons
        /// </summary>
        /// <param name="subjectName"></param>
        private static void SubjectDetailsState(string? subjectName)
        {
            bool subjectExists = false;
            foreach (var subject in _subjects)
            {
                if (subject.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase))
                {
                    subjectExists = true;
                    subject.LoadLessons(_storageService);

                    Console.WriteLine($"\n=== Subject Details: {subject.Name} ===");
                    Console.WriteLine($"Sphere: {subject.Sphere}");
                    Console.WriteLine($"Credits: {subject.Credits}");

                    Console.WriteLine($"Total Duration: {(int)subject.DurationAll.TotalHours} hours {subject.DurationAll.Minutes} minutes");
                    
                    Console.WriteLine($"\nLessons in {subject.Name}:");
                    if (subject.Lessons.Count == 0)
                    {
                        Console.WriteLine("No lessons scheduled yet.");
                    }
                    else
                    {
                        foreach (var lesson in subject.Lessons)
                        {
                            Console.WriteLine(lesson);
                        }
                    }
                }
            }
            if (!subjectExists)
                Console.WriteLine("Subject not found. Please try again.");
            else
            {
                Console.WriteLine("\nType Back to get list of all Subjects.");
                _appState = AppState.End;
            }
        }
    }
}