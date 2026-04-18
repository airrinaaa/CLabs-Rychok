using ClassManager.DTOModels.Lessons;

namespace ClassManager.Services
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonListDTO>> GetLessonsBySubjectAsync(Guid subjectId);
        Task<LessonDetailsDTO?> GetLessonAsync(Guid lessonId);

        Task CreateLessonAsync(LessonCreateDTO lessonCreateDTO);
        Task UpdateLessonAsync(LessonUpdateDTO lessonUpdateDTO);
        Task DeleteLessonAsync(Guid lessonId);
    }
}