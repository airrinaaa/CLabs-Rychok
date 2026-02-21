using ClassManager.DBModels;

namespace ClassManager.Services;
// this class helps to get data from the fake storage
public class StorageService
{
    private List<SubjectDBModel> _subjects;
    private List<LessonDBModel> _lessons;
    /// <summary>
    /// this method loads data from FakeStorage if haven`t loaded it yet
    /// </summary>
    private void LoadData()
    {
        if (_subjects != null && _lessons != null)
            return;
        _subjects = FakeStorage.Subjects.ToList();
        _lessons = FakeStorage.Lessons.ToList();


    }
    /// <summary>
    /// this method returns a list of all subjects
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SubjectDBModel> GetAllSubjects()
    {
        LoadData();
        var resultList = new List<SubjectDBModel>();
        foreach (var subject in _subjects)
        {
            resultList.Add(subject);
        }
        return resultList;
    }
    /// <summary>
    /// this method returns a list of lessons for one specific subject
    /// </summary>
    /// <param name="subjectId"></param>
    /// <returns></returns>
    public IEnumerable<LessonDBModel> GetLessons(Guid subjectId)
    {
        LoadData();
        var resultList = new List<LessonDBModel>();
        foreach (var lesson in _lessons)
        {
            if (lesson.SubjectId == subjectId)
                resultList.Add(lesson);
        }
        return resultList;
    }
}