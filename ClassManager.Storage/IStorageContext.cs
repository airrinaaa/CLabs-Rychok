using ClassManager.DBModels;

namespace ClassManager.Storage
{
    public interface IStorageContext
    {
        IAsyncEnumerable<SubjectDBModel> GetSubjectsAsync();
        Task<SubjectDBModel?> GetSubjectAsync(Guid subjectId);

        Task<IEnumerable<LessonDBModel>> GetLessonsBySubjectAsync(Guid subjectId);
        Task<TimeSpan> GetTotalDurationBySubjectAsync(Guid subjectId);
        Task<LessonDBModel?> GetLessonAsync(Guid lessonId);

        Task SaveLessonAsync(LessonDBModel lesson);
        Task UpdateLessonAsync(LessonDBModel lesson);
        Task DeleteLessonAsync(Guid lessonId);

        Task SaveSubjectAsync(SubjectDBModel subject);
        Task UpdateSubjectAsync(SubjectDBModel subject);
        Task DeleteSubjectAsync(Guid subjectId);
    }
}