using ClassManager.DBModels;
using ClassManager.Storage;

namespace ClassManager.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IStorageContext _storageContext;

        public SubjectRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public IAsyncEnumerable<SubjectDBModel> GetSubjectsAsync()
        {
            return _storageContext.GetSubjectsAsync();
        }

        public Task<SubjectDBModel?> GetSubjectAsync(Guid subjectId)
        {
            return _storageContext.GetSubjectAsync(subjectId);
        }

        public Task SaveSubjectAsync(SubjectDBModel subject)
        {
            return _storageContext.SaveSubjectAsync(subject);
        }

        public Task UpdateSubjectAsync(SubjectDBModel subject)
        {
            return _storageContext.UpdateSubjectAsync(subject);
        }

        public Task DeleteSubjectAsync(Guid subjectId)
        {
            return _storageContext.DeleteSubjectAsync(subjectId);
        }
    }
}