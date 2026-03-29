using ClassManager.DBModels;
using ClassManager.Storage;
using System.Collections.Generic;

namespace ClassManager.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IStorageContext _storageContext;

        public SubjectRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public IEnumerable<SubjectDBModel> GetSubjects()
        {
            return _storageContext.GetSubjects();
        }
        public SubjectDBModel? GetSubject(Guid subjectId)
        {
            return _storageContext.GetSubject(subjectId);
        }
    }
}