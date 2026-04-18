using ClassManager.DTOModels.Subjects;

namespace ClassManager.Services
{
    public interface ISubjectService
    {
        IAsyncEnumerable<SubjectListDTO> GetAllSubjectsAsync();
        Task<SubjectDetailsDTO?> GetSubjectAsync(Guid subjectId);

        Task CreateSubjectAsync(SubjectCreateDTO subjectCreateDTO);
        Task UpdateSubjectAsync(SubjectUpdateDTO subjectUpdateDTO);
        Task DeleteSubjectAsync(Guid subjectId);
    }
}