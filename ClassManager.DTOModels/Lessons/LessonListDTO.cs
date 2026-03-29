using System;
using ClassManager.Common.Enums;

namespace ClassManager.DTOModels.Lessons
{
    public class LessonListDTO
    {
        public Guid Id { get; }
        public string Topic { get; set; }
        public DateTime Date { get; set; }
        public LessonType Type { get; set; }

        public LessonListDTO(Guid id, string topic, DateTime date, LessonType type)
        {
            Id = id;
            Topic = topic;
            Date = date;
            Type = type;
        }
    }
}