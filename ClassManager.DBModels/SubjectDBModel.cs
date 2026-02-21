
using ClassManager.Common.Enums;

namespace ClassManager.DBModels
{
    // this class is a database model for a subject? it only stores data
    public class SubjectDBModel
    {
        // id is generated only once during the creation of the object and cannot be changed later
        public Guid Id { get; }
        public string Name { get; set; }
        public double Credits { get; set; }
        public SubjectSphere Sphere { get; set; }

        // empty constructor
        private SubjectDBModel()
        {
        }
        /// <summary>
        /// constructor that creates a new subject and generates a new id
        /// </summary>
        /// <param name="name"></param>
        /// <param name="credits"></param>
        /// <param name="sphere"></param>
        public SubjectDBModel(string name, double credits, SubjectSphere sphere)
        {
            Id = Guid.NewGuid();
            Name = name;
            Credits = credits;
            Sphere = sphere;
        }
    }
}