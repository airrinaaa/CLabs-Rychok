using ClassManager.Common.Enums;

namespace ClassManager.DTOModels.Subjects
{
    public class SubjectUpdateDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public double Credits { get; }
        public SubjectSphere? Sphere { get; }

        public SubjectUpdateDTO(Guid id, string name, double credits, SubjectSphere? sphere)
        {
            Id = id;
            Name = name;
            Credits = credits;
            Sphere = sphere;
        }
    }
}