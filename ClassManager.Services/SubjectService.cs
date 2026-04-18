using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClassManager.DBModels;
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
        public async IAsyncEnumerable<SubjectListDTO> GetAllSubjectsAsync()
        {
            await foreach (var subject in _subjectRepository.GetSubjectsAsync())
            {
                yield return new SubjectListDTO(subject.Id, subject.Name, subject.Credits, subject.Sphere);
            }
        }

        /// <summary>
        /// gets detailed information about a single subject by its ID
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public async Task<SubjectDetailsDTO?> GetSubjectAsync(Guid subjectId)
        {
            var subject = await _subjectRepository.GetSubjectAsync(subjectId);

            if (subject is null)
                return null;

            var totalDuration = await _lessonRepository.GetTotalDurationBySubjectAsync(subjectId);

            return new SubjectDetailsDTO(subject.Id, subject.Name, subject.Credits, subject.Sphere, totalDuration);
        }

        public async Task CreateSubjectAsync(SubjectCreateDTO subjectCreateDTO)
        {
            var errors = subjectCreateDTO.Validate();

            if (errors.Count > 0)
            {
                throw new ValidationException(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));
            }

            var newSubject = new SubjectDBModel(subjectCreateDTO.Name.Trim(), subjectCreateDTO.Credits, subjectCreateDTO.Sphere!.Value);

            await _subjectRepository.SaveSubjectAsync(newSubject);
        }

        public async Task UpdateSubjectAsync(SubjectUpdateDTO subjectUpdateDTO)
        {
            var errors = subjectUpdateDTO.Validate();

            if (errors.Count > 0)
            {
                throw new ValidationException(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));
            }

            var existingSubject = await _subjectRepository.GetSubjectAsync(subjectUpdateDTO.Id);

            if (existingSubject is null)
            {
                throw new KeyNotFoundException("Subject was not found.");
            }

            var updatedSubject = new SubjectDBModel(subjectUpdateDTO.Id, subjectUpdateDTO.Name.Trim(), subjectUpdateDTO.Credits, subjectUpdateDTO.Sphere!.Value);
            await _subjectRepository.UpdateSubjectAsync(updatedSubject);
        }

        public async Task DeleteSubjectAsync(Guid subjectId)
        {
            var existingSubject = await _subjectRepository.GetSubjectAsync(subjectId);

            if (existingSubject is null)
            {
                throw new KeyNotFoundException("Subject was not found.");
            }

            await _subjectRepository.DeleteSubjectAsync(subjectId);
        }
    }
}