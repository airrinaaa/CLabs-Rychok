using ClassManager.DBModels;
using ClassManager.Storage;
using System;
using System.Collections.Generic;

namespace ClassManager.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly IStorageContext _storageContext;

        public LessonRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public IEnumerable<LessonDBModel> GetLessonsBySubject(Guid subjectId)
        {
            return _storageContext.GetLessonsBySubject(subjectId);
        }

        public TimeSpan GetTotalDurationBySubject(Guid subjectId)
        {
            return _storageContext.GetTotalDurationBySubject(subjectId);
        }

        public LessonDBModel? GetLesson(Guid lessonId)
        {
            return _storageContext.GetLesson(lessonId);
        }
    }
}