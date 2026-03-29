using ClassManager.DBModels;
using System.Collections.Generic;

namespace ClassManager.Repositories
{
    public interface ISubjectRepository
    {
        IEnumerable<SubjectDBModel> GetSubjects();
        SubjectDBModel? GetSubject(Guid subjectId);
    }
}