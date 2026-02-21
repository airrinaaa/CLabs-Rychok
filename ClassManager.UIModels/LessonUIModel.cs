using ClassManager.Common.Enums;
using ClassManager.DBModels;

// this class is a UI model for a lesson, it is used to view and edit data
namespace ClassManager.UIModels
{

    public class LessonUIModel
    {
        private LessonDBModel _dbModel;
        private Guid _subjectId;
        private string _topic;
        private LessonType _type;
        private DateTime _date;
        private TimeSpan _startTime; 
        private TimeSpan _endTime;
        private TimeSpan _lessonDuration;
        public Guid? Id 
        { 
            get => _dbModel?.Id;
        }

        public Guid SubjectId 
        { 
            get => _subjectId; 
        }

        public DateTime Date 
        { 
            get => _date; 
            set => _date = value; 
        }

        public TimeSpan StartTime 
        { 
            get => _startTime; 
            set 
            {
                _startTime = value; 
                CalculateLessonDuration(); //recalculate lesson duration every time when start time of the lesson changes
            }

        }

        public TimeSpan EndTime 
        { 
            get => _endTime; 
            set
            {
                _endTime = value;
                CalculateLessonDuration(); //recalculate lesson duration every time when end time of the lesson changes
            }
        }
        public TimeSpan LessonDuration
        {
            get => _lessonDuration;
        }

        public string Topic 
        { 
            get => _topic; 
            set => _topic = value; 
        }
        // prevent changing the LessonType if the entity already exists in the database
        // it`s because lectures are typically scheduled for an entire students` stream, 
        // while seminars and labs are meant for specific smaller groups of students
        // changing the type on the lesson would break the scheduling and group assignment logic
        public LessonType Type 
        { 
            get => _type; 
            set 
            {
                if (_dbModel != null) 
                    return; 
                
                _type = value; 
            }
        }
        /// <summary>
        /// creates a new lesson
        /// </summary>
        /// <param name="subjectId"></param>
        public LessonUIModel(Guid subjectId)
        {
            _subjectId = subjectId;
        }
        /// <summary>
        /// load the existing lesson from the database
        /// </summary>
        /// <param name="dbModel"></param>
        public LessonUIModel(LessonDBModel dbModel)
        {
            _dbModel = dbModel;
            _subjectId = dbModel.SubjectId;
            _topic = dbModel.Topic;
            _type = dbModel.Type;
            _date = dbModel.Date;
            _startTime = dbModel.StartTime; 
            _endTime = dbModel.EndTime; 
            CalculateLessonDuration();
        }

        /// <summary>
        /// this method calculates how long the lesson is 
        /// </summary>
        private void CalculateLessonDuration()
        {
            _lessonDuration = _endTime - _startTime;
        }
        /// <summary>
        /// this method saves our changes back to the database model
        /// </summary>
        public void SaveChangesToDBModel()
        {
            if (_dbModel != null)
            {
                _dbModel.Topic = _topic;
                _dbModel.Date = _date;
                _dbModel.StartTime = _startTime;
                _dbModel.EndTime = _endTime;
            }
            else
            {
                _dbModel = new LessonDBModel(_subjectId, _date, _startTime, _endTime, _topic, _type);
            }
        }

        public override string ToString()
        {
            return $"[{Date:dd.MM.yyyy}] {StartTime:hh\\:mm}-{EndTime:hh\\:mm} | {Topic} ({Type}) | Duration: {LessonDuration.TotalMinutes} min.";
        }

    }
}
