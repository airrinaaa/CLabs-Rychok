using ClassManager.DBModels;
using System;
using System.Collections.Generic;

namespace ClassManager.Storage
{
    public interface IStorageContext
    {
        IEnumerable<SubjectDBModel> GetSubjects();
        IEnumerable<LessonDBModel> GetLessonsBySubject(Guid subjectId);
        SubjectDBModel? GetSubject(Guid subjectId);
        TimeSpan GetTotalDurationBySubject(Guid subjectId);
        LessonDBModel? GetLesson(Guid lessonId);
    }
}