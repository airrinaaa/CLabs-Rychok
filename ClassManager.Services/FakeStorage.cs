using System.Collections.Generic;
using ClassManager.DBModels;
using ClassManager.Common.Enums;
namespace ClassManager.Services;
// this static class is a fake database, it holds test data
internal static class FakeStorage
{
    private static readonly List<SubjectDBModel> _subjects;
    private static readonly List<LessonDBModel> _lessons;
    // this property gets the list of all subjects
    internal static IEnumerable<SubjectDBModel> Subjects
    {
        get
        {
            return _subjects.ToList();
        }
    }
    /// this property gets the list of all lessons.
    internal static IEnumerable<LessonDBModel> Lessons
    {
        get
        {
            return _lessons.ToList();
        }
    }
    /// <summary>
    /// it`s static constructor that adds test data when the program starts
    /// </summary>
    static FakeStorage()
    {
        var subjectDatabases = new SubjectDBModel("Database Systems", 5.0, SubjectSphere.Engineering);
        var subjectDiscreteMath = new SubjectDBModel("Discrete Mathematics", 6.0, SubjectSphere.Mathematics);
        var subjectProgramming = new SubjectDBModel("Programming", 6.5, SubjectSphere.Programming);

        _subjects = new List<SubjectDBModel>
        {
            subjectDatabases,
            subjectDiscreteMath,
            subjectProgramming
        };

        _lessons = new List<LessonDBModel>();

        _lessons.Add(new LessonDBModel(
            subjectDatabases.Id,
            new DateTime(2026, 03, 01),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 20, 0),
            "Introduction to SQL",
            LessonType.Lecture
        ));

        _lessons.Add(new LessonDBModel(
            subjectDatabases.Id,
            new DateTime(2026, 03, 08),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 20, 0),
            "Relational Algebra",
            LessonType.Practice
        ));

        _lessons.Add(new LessonDBModel(
            subjectDiscreteMath.Id,
            new DateTime(2026, 03, 02),
            new TimeSpan(12, 0, 0),
            new TimeSpan(13, 20, 0),
            "Graph Theory Basics",
            LessonType.Seminar
        ));

        _lessons.Add(new LessonDBModel(
            subjectDiscreteMath.Id,
            new DateTime(2026, 03, 09),
            new TimeSpan(12, 0, 0),
            new TimeSpan(13, 20, 0),
            "Combinatorics Basics",
            LessonType.Lecture
        ));

        _lessons.Add(new LessonDBModel(subjectProgramming.Id, new DateTime(2026, 03, 03), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "C# OOP Principles", LessonType.Practice));
        _lessons.Add(new LessonDBModel(subjectProgramming.Id, new DateTime(2026, 03, 04), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Classes and Objects", LessonType.Lecture));
        _lessons.Add(new LessonDBModel(subjectProgramming.Id, new DateTime(2026, 03, 05), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Constructors", LessonType.Practice));
        _lessons.Add(new LessonDBModel(subjectProgramming.Id, new DateTime(2026, 03, 10), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Properties and Fields", LessonType.Lecture));
        _lessons.Add(new LessonDBModel(subjectProgramming.Id, new DateTime(2026, 03, 11), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Methods and Parameters", LessonType.Practice));
        _lessons.Add(new LessonDBModel(subjectProgramming.Id, new DateTime(2026, 03, 12), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Value vs Reference Types", LessonType.Lecture));
        _lessons.Add(new LessonDBModel(subjectProgramming.Id, new DateTime(2026, 03, 17), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Collections (List, Array)", LessonType.Practice));
        _lessons.Add(new LessonDBModel(subjectProgramming.Id, new DateTime(2026, 03, 18), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "LINQ Basics", LessonType.Lecture));
        _lessons.Add(new LessonDBModel(subjectProgramming.Id, new DateTime(2026, 03, 19), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Exception Handling", LessonType.Practice));
        _lessons.Add(new LessonDBModel(subjectProgramming.Id, new DateTime(2026, 03, 24), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Interfaces", LessonType.Lecture));
    }

}