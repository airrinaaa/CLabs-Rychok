using System;
using System.Collections.Generic;
using ClassManager.DTOModels.Lessons;
using ClassManager.Repositories;

namespace ClassManager.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        /// <summary>
        /// initializes a new instance of the LessonService
        /// </summary>
        /// <param name="lessonRepository"></param>
        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        /// <summary>
        /// gets a list of short lesson models for a specific subject
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public IEnumerable<LessonListDTO> GetLessonsBySubject(Guid subjectId)
        {
            foreach (var lesson in _lessonRepository.GetLessonsBySubject(subjectId))
            {
                yield return new LessonListDTO(
                    lesson.Id, 
                    lesson.Topic, 
                    lesson.Date, 
                    lesson.Type
                );
            }
        }

        /// <summary>
        /// gets detailed information about a single lesson by its ID
        /// </summary>
        /// <param name="lessonId"></param>
        /// <returns></returns>
        public LessonDetailsDTO? GetLesson(Guid lessonId)
        {
            var lesson = _lessonRepository.GetLesson(lessonId); 
            
            if (lesson == null) 
                return null;
            return new LessonDetailsDTO(
                lesson.Id,
                lesson.Topic,
                lesson.Date,
                lesson.StartTime,
                lesson.EndTime,
                lesson.Type
            );
        }
    }
}