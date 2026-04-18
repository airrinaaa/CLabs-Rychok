using System.ComponentModel.DataAnnotations;
using ClassManager.DBModels;
using ClassManager.DTOModels.Lessons;
using ClassManager.Repositories;

namespace ClassManager.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<IEnumerable<LessonListDTO>> GetLessonsBySubjectAsync(Guid subjectId)
        {
            return (await _lessonRepository.GetLessonsBySubjectAsync(subjectId))
                .Select(lesson => new LessonListDTO(lesson.Id, lesson.Topic, lesson.Date, lesson.Type));
        }

        public async Task<LessonDetailsDTO?> GetLessonAsync(Guid lessonId)
        {
            var lesson = await _lessonRepository.GetLessonAsync(lessonId);

            return lesson is null
                ? null
                : new LessonDetailsDTO(lesson.Id, lesson.SubjectId, lesson.Topic, lesson.Date, lesson.StartTime, lesson.EndTime, lesson.Type);
        }

        public async Task CreateLessonAsync(LessonCreateDTO lessonCreateDTO)
        {
            var errors = lessonCreateDTO.Validate();

            if (errors.Count > 0)
            {
                throw new ValidationException(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));
            }

            var newLesson = new LessonDBModel(lessonCreateDTO.SubjectId, lessonCreateDTO.Date, lessonCreateDTO.StartTime, lessonCreateDTO.EndTime, lessonCreateDTO.Topic.Trim(), lessonCreateDTO.Type!.Value);
            await _lessonRepository.SaveLessonAsync(newLesson);
        }

        public async Task UpdateLessonAsync(LessonUpdateDTO lessonUpdateDTO)
        {
            var errors = lessonUpdateDTO.Validate();
            if (errors.Count > 0)
            {
                throw new ValidationException(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));
            }

            var existingLesson = await _lessonRepository.GetLessonAsync(lessonUpdateDTO.Id);

            if (existingLesson is null)
            {
                throw new KeyNotFoundException("Lesson was not found.");
            }

            var updatedLesson = new LessonDBModel(lessonUpdateDTO.Id, lessonUpdateDTO.SubjectId, lessonUpdateDTO.Date, lessonUpdateDTO.StartTime, lessonUpdateDTO.EndTime, lessonUpdateDTO.Topic.Trim(), lessonUpdateDTO.Type!.Value);
            await _lessonRepository.UpdateLessonAsync(updatedLesson);
        }

        public async Task DeleteLessonAsync(Guid lessonId)
        {
            var existingLesson = await _lessonRepository.GetLessonAsync(lessonId);

            if (existingLesson is null)
            {
                throw new KeyNotFoundException("Lesson was not found.");
            }
            await _lessonRepository.DeleteLessonAsync(lessonId);
        }
    }
}