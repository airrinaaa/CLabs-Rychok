using System;
using System.Collections.Generic;
using ClassManager.DTOModels.Subjects;
using ClassManager.Repositories;

namespace ClassManager.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILessonRepository _lessonRepository;

        /// <summary>
        /// initializes the service
        /// </summary>
        /// <param name="subjectRepository"></param>
        /// <param name="lessonRepository"></param>
        public SubjectService(ISubjectRepository subjectRepository, ILessonRepository lessonRepository)
        {
            _subjectRepository = subjectRepository;
            _lessonRepository = lessonRepository;
        }

        /// <summary>
        /// gets a list of all subjects
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubjectListDTO> GetAllSubjects()
        {
            foreach (var subject in _subjectRepository.GetSubjects())
            {
                yield return new SubjectListDTO(
                    subject.Id,
                    subject.Name,
                    subject.Credits
                );
            }
        }

        /// <summary>
        /// gets detailed information about a single subject by its ID
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public SubjectDetailsDTO? GetSubject(Guid subjectId)
        {
            var subject = _subjectRepository.GetSubject(subjectId);

            if (subject is null)
                return null;

            var totalDuration = _lessonRepository.GetTotalDurationBySubject(subjectId);

            return new SubjectDetailsDTO(
                subject.Id,
                subject.Name,
                subject.Credits,
                subject.Sphere,
                totalDuration
            );
        }
    }
}