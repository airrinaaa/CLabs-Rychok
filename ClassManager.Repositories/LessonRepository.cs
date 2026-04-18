using ClassManager.DBModels;
using ClassManager.Storage;

namespace ClassManager.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly IStorageContext _storageContext;

        public LessonRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public Task<IEnumerable<LessonDBModel>> GetLessonsBySubjectAsync(Guid subjectId)
        {
            return _storageContext.GetLessonsBySubjectAsync(subjectId);
        }

        public Task<TimeSpan> GetTotalDurationBySubjectAsync(Guid subjectId)
        {
            return _storageContext.GetTotalDurationBySubjectAsync(subjectId);
        }

        public Task<LessonDBModel?> GetLessonAsync(Guid lessonId)
        {
            return _storageContext.GetLessonAsync(lessonId);
        }

        public Task SaveLessonAsync(LessonDBModel lesson)
        {
            return _storageContext.SaveLessonAsync(lesson);
        }

        public Task UpdateLessonAsync(LessonDBModel lesson)
        {
            return _storageContext.UpdateLessonAsync(lesson);
        }

        public Task DeleteLessonAsync(Guid lessonId)
        {
            return _storageContext.DeleteLessonAsync(lessonId);
        }
    }
}