using ClassManager.DBModels;

namespace ClassManager.Services;

/// <summary>
/// Interface for storage service providing access to subjects and lessons data.
/// Implements Inversion of Control principle for dependency injection.
/// </summary>
[Obsolete("This class was created for testing and learning purposes. It is not longer needed")]
public interface IStorageService
{
    /// <summary>
    /// Retrieves all subjects from the storage.
    /// </summary>
    /// <returns>Enumerable collection of SubjectDBModel objects.</returns>
    IEnumerable<SubjectDBModel> GetAllSubjects();

    /// <summary>
    /// Retrieves all lessons for a specific subject from the storage.
    /// </summary>
    /// <param name="subjectId">The unique identifier of the subject.</param>
    /// <returns>Enumerable collection of LessonDBModel objects.</returns>
    IEnumerable<LessonDBModel> GetLessons(Guid subjectId);
}
