using ClassManager.Common.Enums;

namespace ClassManager.DBModels
{
    // this class is a database model for a subject? it only stores data
    public class SubjectDBModel
    {
        // id is generated only once during the creation of the object and cannot be changed later
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Credits { get; set; }
        public SubjectSphere Sphere { get; set; }

        // empty constructor
        public SubjectDBModel()
        {
        }

        /// <summary>
        /// constructor that creates a new subject and generates a new id
        /// </summary>
        /// <param name="name"></param>
        /// <param name="credits"></param>
        /// <param name="sphere"></param>
        public SubjectDBModel(string name, double credits, SubjectSphere sphere) : this(Guid.NewGuid(), name, credits, sphere)
        {
        }

        /// <summary>
        /// constructor that creates a new subject and generates a new id
        /// </summary>
        /// <param name="name"></param>
        /// <param name="credits"></param>
        /// <param name="sphere"></param>
        public SubjectDBModel(Guid guid, string name, double credits, SubjectSphere sphere)
        {
            Id = guid;
            Name = name;
            Credits = credits;
            Sphere = sphere;
        }
    }
}