using ClassManager.Common.Enums;

namespace ClassManager.DTOModels.Lessons
{
    public class LessonCreateDTO
    {
        public Guid SubjectId { get; }
        public DateTime Date { get; }
        public TimeSpan StartTime { get; }
        public TimeSpan EndTime { get; }
        public string Topic { get; }
        public LessonType? Type { get; }

        public LessonCreateDTO(Guid subjectId, DateTime date, TimeSpan startTime, TimeSpan endTime, string topic, LessonType? type)
        {
            SubjectId = subjectId;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Topic = topic;
            Type = type;
        }
    }
}