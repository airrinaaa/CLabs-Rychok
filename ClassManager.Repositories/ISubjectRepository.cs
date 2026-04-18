using ClassManager.DBModels;

namespace ClassManager.Repositories
{
    public interface ISubjectRepository
    {
        IAsyncEnumerable<SubjectDBModel> GetSubjectsAsync();
        Task<SubjectDBModel?> GetSubjectAsync(Guid subjectId);

        Task SaveSubjectAsync(SubjectDBModel subject);
        Task UpdateSubjectAsync(SubjectDBModel subject);
        Task DeleteSubjectAsync(Guid subjectId);
    }
}