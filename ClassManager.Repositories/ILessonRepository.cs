using ClassManager.DBModels;

namespace ClassManager.Repositories
{
    public interface ILessonRepository
    {
        Task<IEnumerable<LessonDBModel>> GetLessonsBySubjectAsync(Guid subjectId);
        Task<TimeSpan> GetTotalDurationBySubjectAsync(Guid subjectId);
        Task<LessonDBModel?> GetLessonAsync(Guid lessonId);

        Task SaveLessonAsync(LessonDBModel lesson);
        Task UpdateLessonAsync(LessonDBModel lesson);
        Task DeleteLessonAsync(Guid lessonId);
    }
}