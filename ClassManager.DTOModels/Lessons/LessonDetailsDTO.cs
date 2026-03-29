using System;
using ClassManager.Common.Enums;

namespace ClassManager.DTOModels.Lessons
{
    public class LessonDetailsDTO
    {
        public Guid Id { get; }
        public string Topic { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public LessonType Type { get; set; }

        public TimeSpan LessonDuration => EndTime - StartTime;
    
        public string LessonInfo =>
            $"This {Type.ToString().ToLower()} on {Topic} is scheduled for {Date:dddd, MMMM d, yyyy}. " +
            $"The session starts at {StartTime:hh\\:mm} and ends at {EndTime:hh\\:mm}.";

        public LessonDetailsDTO(Guid id, string topic, DateTime date, TimeSpan startTime, TimeSpan endTime, LessonType type)
        {
            Id = id;
            Topic = topic;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
            Type = type;
        }
    }
}