using ClassManager.Common.Enums;

namespace ClassManager.DTOModels.Subjects
{
    public class SubjectCreateDTO
    {
        public string Name { get; }
        public double Credits { get; }
        public SubjectSphere? Sphere { get; }

        public SubjectCreateDTO(string name, double credits, SubjectSphere? sphere)
        {
            Name = name;
            Credits = credits;
            Sphere = sphere;
        }
    }
}