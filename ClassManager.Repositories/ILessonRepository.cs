using ClassManager.DBModels;
using System;
using System.Collections.Generic;

namespace ClassManager.Repositories
{
    public interface ILessonRepository
    {
        IEnumerable<LessonDBModel> GetLessonsBySubject(Guid subjectId);
        TimeSpan GetTotalDurationBySubject(Guid subjectId);
        LessonDBModel? GetLesson(Guid lessonId);
    }
}