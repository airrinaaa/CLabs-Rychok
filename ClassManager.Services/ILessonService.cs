using System;
using System.Collections.Generic;
using ClassManager.DTOModels.Lessons;

namespace ClassManager.Services
{
    public interface ILessonService
    {
        IEnumerable<LessonListDTO> GetLessonsBySubject(Guid subjectId);
        public LessonDetailsDTO? GetLesson(Guid lessonId);
    }
}