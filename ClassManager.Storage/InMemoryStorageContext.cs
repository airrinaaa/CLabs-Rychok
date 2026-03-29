using ClassManager.Common.Enums;
using ClassManager.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassManager.Storage
{
    public class InMemoryStorageContext : IStorageContext
    {
        private record SubjectRecord(Guid Id, string Name, double Credits, SubjectSphere Sphere);
        private record LessonRecord(Guid Id, Guid SubjectId, DateTime Date, TimeSpan StartTime, TimeSpan EndTime, string Topic, LessonType Type);

        private static readonly List<SubjectRecord> _subjects = new List<SubjectRecord>();
        private static readonly List<LessonRecord> _lessons = new List<LessonRecord>();

        static InMemoryStorageContext()
        {
            MockStoragePopulation();
        }

        private static void MockStoragePopulation()
        {
            //генеруємо статичні Id для предметів, щоб потім прив'язати до них заняття
            var dbId = Guid.NewGuid();
            var mathId = Guid.NewGuid();
            var progId = Guid.NewGuid();

            //додаємо предмети
            _subjects.Add(new SubjectRecord(dbId, "Database Systems", 5.0, SubjectSphere.Engineering));
            _subjects.Add(new SubjectRecord(mathId, "Discrete Mathematics", 6.0, SubjectSphere.Mathematics));
            _subjects.Add(new SubjectRecord(progId, "Programming", 6.5, SubjectSphere.Programming));

            //додаємо заняття для Database Systems
            _lessons.Add(new LessonRecord(Guid.NewGuid(), dbId, new DateTime(2026, 03, 01), new TimeSpan(10, 0, 0), new TimeSpan(11, 20, 0), "Introduction to SQL", LessonType.Lecture));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), dbId, new DateTime(2026, 03, 08), new TimeSpan(10, 0, 0), new TimeSpan(11, 20, 0), "Relational Algebra", LessonType.Practice));

            //додаємо заняття для Discrete Mathematics
            _lessons.Add(new LessonRecord(Guid.NewGuid(), mathId, new DateTime(2026, 03, 02), new TimeSpan(12, 0, 0), new TimeSpan(13, 20, 0), "Graph Theory Basics", LessonType.Seminar));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), mathId, new DateTime(2026, 03, 09), new TimeSpan(12, 0, 0), new TimeSpan(13, 20, 0), "Combinatorics Basics", LessonType.Lecture));

            //додаємо заняття для Programming
            _lessons.Add(new LessonRecord(Guid.NewGuid(), progId, new DateTime(2026, 03, 03), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "C# OOP Principles", LessonType.Practice));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), progId, new DateTime(2026, 03, 04), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Classes and Objects", LessonType.Lecture));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), progId, new DateTime(2026, 03, 05), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Constructors", LessonType.Practice));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), progId, new DateTime(2026, 03, 10), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Properties and Fields", LessonType.Lecture));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), progId, new DateTime(2026, 03, 11), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Methods and Parameters", LessonType.Practice));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), progId, new DateTime(2026, 03, 12), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Value vs Reference Types", LessonType.Lecture));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), progId, new DateTime(2026, 03, 17), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Collections (List, Array)", LessonType.Practice));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), progId, new DateTime(2026, 03, 18), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "LINQ Basics", LessonType.Lecture));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), progId, new DateTime(2026, 03, 19), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Exception Handling", LessonType.Practice));
            _lessons.Add(new LessonRecord(Guid.NewGuid(), progId, new DateTime(2026, 03, 24), new TimeSpan(14, 0, 0), new TimeSpan(15, 30, 0), "Interfaces", LessonType.Lecture));
        }

        public IEnumerable<SubjectDBModel> GetSubjects()
        {
            foreach (var subject in _subjects)
            {
                yield return new SubjectDBModel(subject.Id, subject.Name, subject.Credits, subject.Sphere);
            }
        }

        public IEnumerable<LessonDBModel> GetLessonsBySubject(Guid subjectId)
        {
            return _lessons
                .Where(lesson => lesson.SubjectId == subjectId)
                .Select(lesson => new LessonDBModel(lesson.Id, lesson.SubjectId, lesson.Date, lesson.StartTime, lesson.EndTime, lesson.Topic, lesson.Type));
        }

        public SubjectDBModel GetSubject(Guid subjectId)
        {
            var subject = _subjects.FirstOrDefault(s => s.Id == subjectId);
            return subject is null ? null : new SubjectDBModel(subject.Id, subject.Name, subject.Credits, subject.Sphere);
        }

        public TimeSpan GetTotalDurationBySubject(Guid subjectId)
        {
            long totalTicks = _lessons
                .Where(lesson => lesson.SubjectId == subjectId)
                .Sum(lesson => (lesson.EndTime - lesson.StartTime).Ticks);

            return new TimeSpan(totalTicks);
        }
       public LessonDBModel? GetLesson(Guid lessonId)
        {
            var record = _lessons.FirstOrDefault(l => l.Id == lessonId); 
            
            if (record == null) 
                return null;
            return new LessonDBModel(
                record.Id,
                record.SubjectId,
                record.Date,
                record.StartTime,
                record.EndTime,
                record.Topic,
                record.Type
            );
        }
    }
}