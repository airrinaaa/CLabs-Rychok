using System;

namespace ClassManager.DTOModels.Subjects
{
    public class SubjectListDTO
    {
        public Guid Id { get; }
        public string Name { get; }
        public double Credits { get; }

        public SubjectListDTO(Guid id, string name, double credits)
        {
            Id = id;
            Name = name;
            Credits = credits;
        }
    }
}