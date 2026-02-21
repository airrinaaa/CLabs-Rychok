using ClassManager.Common.Enums;
using ClassManager.Services;
using ClassManager.DBModels;

// this class is a UI model for a subject, it is used to view and edit data
namespace ClassManager.UIModels
{

    public class SubjectUIModel
    {
        private SubjectDBModel _dbModel;
        private string _name;
        private double _credits;
        private SubjectSphere _sphere;
        
        private List<LessonUIModel> _lessons;
        public Guid? Id 
        { 
            get => _dbModel?.Id;
        }
        public string Name
        { 
            get => _name; 
            set => _name = value; 
        }

        public double Credits 
        { 
            get => _credits; 
            set => _credits = value;
        }

        public SubjectSphere Sphere 
        { 
            get => _sphere; 
            set => _sphere = value;
        }

        public IReadOnlyList<LessonUIModel> Lessons 
        {
            get => _lessons;
        }
        // calculates the total time of all lessons in this subject
        public TimeSpan DurationAll 
        {
            get 
            {
                TimeSpan total = TimeSpan.Zero; 
                foreach (var lesson in _lessons)
                {
                    total += lesson.LessonDuration;
                }
                return total;
            }
        }
        /// <summary>
        /// creates a new empty subject and a list for its lessons
        /// </summary>
        public SubjectUIModel()
        {
            _lessons = new List<LessonUIModel>();
        }

        /// <summary>
        /// loads an existing subject from the database
        /// </summary>
        /// <param name="dbModel"></param>
        public SubjectUIModel(SubjectDBModel dbModel) : this() 
        {
            _dbModel = dbModel;
            _name = dbModel.Name;
            _credits = dbModel.Credits;
            _sphere = dbModel.Sphere;
        }

        /// <summary>
        /// This method saves our changes back to the database model
        /// </summary>
        public void SaveChangesToDBModel()
        {
            if (_dbModel != null)
            {
                _dbModel.Name = _name;
                _dbModel.Credits = _credits;
                _dbModel.Sphere = _sphere;
            }
            else
            {
                _dbModel = new SubjectDBModel(_name, _credits, _sphere);
            }
        }

        /// <summary>
        /// this method loads all lessons for this subject from the storage
        /// </summary>
        /// <param name="storage"></param>
        public void LoadLessons(StorageService storage)
        {
            if (Id == null || _lessons.Count > 0)
                return;
            foreach (var lessonDB in storage.GetLessons(Id.Value))
            {
                _lessons.Add(new LessonUIModel(lessonDB));
            }
        }
        public override string ToString()
        {
            return $"Subject: {Name} | Sphere: {Sphere} | Credits: {Credits} | Lessons: {Lessons.Count}";
        }
    }
}
