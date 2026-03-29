using ClassManager.Common.Enums;
using System;

namespace ClassManager.DTOModels.Subjects
{
    public class SubjectDetailsDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public double Credits { get; }
        public SubjectSphere Sphere { get; }
        public TimeSpan TotalDuration { get; }

        public SubjectDetailsDTO(Guid id, string name, double credits, SubjectSphere sphere, TimeSpan totalDuration)
        {
            Id = id;
            Name = name;
            Credits = credits;
            Sphere = sphere;
            TotalDuration = totalDuration;
        }
    }
}