using ClassManager.Common.Enums;

namespace ClassManager.DBModels
{
    // this class is a database model for a lesson, it only stores data
    public class LessonDBModel
    {
        // id is generated only once during the creation of the object and cannot be changed later
        public Guid Id { get; }

        // subjectId is generated only once during the creation of the object and cannot be changed later
        public Guid SubjectId { get; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Topic { get; set; }

        public LessonType Type { get; set; }

        // empty constructor
        private LessonDBModel()
        {
        }
        /// <summary>
        /// constructor that creates a new lesson and generates a new id
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="date"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="topic"></param>
        /// <param name="type"></param>
        public LessonDBModel(Guid subjectId, DateTime date, TimeSpan startTime, TimeSpan endTime, string topic, LessonType type)
        {
            Id = Guid.NewGuid();
            SubjectId = subjectId;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Topic = topic;
            Type = type;
        }

    }
}