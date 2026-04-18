using System.IO;
using SQLite;
using ClassManager.DBModels;

namespace ClassManager.Storage
{
    public class SQLiteStorageContext : IStorageContext
    {
        private const string DatabaseFileName = "class_manager.db3";
        private static readonly string DatabasePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFileName);

        private SQLiteAsyncConnection? _databaseConnection;
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        private async Task Init()
        {
            await _semaphore.WaitAsync();

            try
            {
                if (_databaseConnection != null)
                    return;

                _databaseConnection = new SQLiteAsyncConnection(DatabasePath);

                await _databaseConnection.CreateTableAsync<SubjectDBModel>();
                await _databaseConnection.CreateTableAsync<LessonDBModel>();

                var subjectsCount = await _databaseConnection.Table<SubjectDBModel>().CountAsync();

                if (subjectsCount == 0)
                {
                    await CreateMockStorage();
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task CreateMockStorage()
        {
            var inMemoryStorage = new InMemoryStorageContext();

            await foreach (var subject in inMemoryStorage.GetSubjectsAsync())
            {
                await _databaseConnection!.InsertAsync(subject);

                var lessons = await inMemoryStorage.GetLessonsBySubjectAsync(subject.Id);

                foreach (var lesson in lessons)
                {
                    await _databaseConnection.InsertAsync(lesson);
                }
            }
        }

        public async IAsyncEnumerable<SubjectDBModel> GetSubjectsAsync()
        {
            await Init();

            var subjects = await _databaseConnection!
                .Table<SubjectDBModel>()
                .ToListAsync();

            foreach (var subject in subjects)
            {
                yield return subject;
            }
        }

        public async Task<SubjectDBModel?> GetSubjectAsync(Guid subjectId)
        {
            await Init();

            return await _databaseConnection!
                .Table<SubjectDBModel>()
                .FirstOrDefaultAsync(subject => subject.Id == subjectId);
        }

        public async Task<IEnumerable<LessonDBModel>> GetLessonsBySubjectAsync(Guid subjectId)
        {
            await Init();

            return await _databaseConnection!
                .Table<LessonDBModel>()
                .Where(lesson => lesson.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<TimeSpan> GetTotalDurationBySubjectAsync(Guid subjectId)
        {
            await Init();

            var lessons = await _databaseConnection!
                .Table<LessonDBModel>()
                .Where(lesson => lesson.SubjectId == subjectId)
                .ToListAsync();

            return new TimeSpan(lessons.Sum(lesson => (lesson.EndTime - lesson.StartTime).Ticks));
        }

        public async Task<LessonDBModel?> GetLessonAsync(Guid lessonId)
        {
            await Init();

            return await _databaseConnection!
                .Table<LessonDBModel>()
                .FirstOrDefaultAsync(lesson => lesson.Id == lessonId);
        }

        public async Task SaveLessonAsync(LessonDBModel lesson)
        {
            await Init();

            await _databaseConnection!.InsertAsync(lesson);
        }

        public async Task DeleteLessonAsync(Guid lessonId)
        {
            await Init();

            await _databaseConnection!.ExecuteAsync(
                "DELETE FROM LessonDBModel WHERE Id = ?",
                lessonId);
        }

        public async Task UpdateLessonAsync(LessonDBModel lesson)
        {
            await Init();

            await _databaseConnection!.ExecuteAsync(
                "UPDATE LessonDBModel SET Date = ?, StartTime = ?, EndTime = ?, Topic = ?, Type = ? WHERE Id = ?",
                lesson.Date,
                lesson.StartTime,
                lesson.EndTime,
                lesson.Topic,
                (int)lesson.Type,
                lesson.Id);
        }

        public async Task SaveSubjectAsync(SubjectDBModel subject)
        {
            await Init();

            await _databaseConnection!.InsertAsync(subject);
        }

        public async Task UpdateSubjectAsync(SubjectDBModel subject)
        {
            await Init();

            await _databaseConnection!.ExecuteAsync(
                "UPDATE SubjectDBModel SET Name = ?, Credits = ?, Sphere = ? WHERE Id = ?",
                subject.Name,
                subject.Credits,
                (int)subject.Sphere,
                subject.Id);
        }

        public async Task DeleteSubjectAsync(Guid subjectId)
        {
            await Init();

            await _databaseConnection!.ExecuteAsync(
                "DELETE FROM LessonDBModel WHERE SubjectId = ?",
                subjectId);

            await _databaseConnection.ExecuteAsync(
                "DELETE FROM SubjectDBModel WHERE Id = ?",
                subjectId);
        }
    }
}