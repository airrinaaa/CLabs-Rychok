using ClassManager.DTOModels.Subjects;
using System.Collections.Generic;

namespace ClassManager.Services
{
    public interface ISubjectService
    {
        IEnumerable<SubjectListDTO> GetAllSubjects();
        public SubjectDetailsDTO? GetSubject(Guid subjectId);
    }
}