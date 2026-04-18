using ClassManager.Common.Enums;
using ClassManager.DTOModels.Lessons;
using ClassManager.DTOModels.Subjects;

namespace ClassManager.Services
{
    public record struct ValidationError(string ErrorMessage, string MemberName);

    public static class Validators
    {
        public static List<ValidationError> Validate(this LessonCreateDTO lessonCreateDTO)
        {
            return ValidateLesson(lessonCreateDTO.Topic, lessonCreateDTO.Type, lessonCreateDTO.Date, lessonCreateDTO.StartTime, lessonCreateDTO.EndTime, true);
        }

        private static List<ValidationError> ValidateLesson(string topic, LessonType? type, DateTime date, TimeSpan startTime, TimeSpan endTime, bool validatePastDate)
        {
            var errors = new List<ValidationError>();

            errors.AddRange(ValidateTopic(topic, nameof(LessonCreateDTO.Topic), "Topic"));

            if (type == null)
            {
                errors.Add(new ValidationError("Lesson type must be selected.", nameof(LessonCreateDTO.Type)));
            }

            if (validatePastDate && date.Date < DateTime.Today)
            {
                errors.Add(new ValidationError("Lesson date cannot be in the past.", nameof(LessonCreateDTO.Date)));
            }

            if (endTime <= startTime)
            {
                errors.Add(new ValidationError("Lesson end time must be later than start time.", nameof(LessonCreateDTO.EndTime)));
            }

            return errors;
        }
        private static List<ValidationError> ValidateTopic(string topic, string propertyName, string displayName)
        {
            var errors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(topic))
            {
                errors.Add(new ValidationError($"{displayName} can't be empty.", propertyName));
                return errors;
            }

            if (topic.Trim().Length < 2)
            {
                errors.Add(new ValidationError($"{displayName} must be at least 2 characters long.", propertyName));
            }

            return errors;
        }

        public static List<ValidationError> Validate(this SubjectCreateDTO subjectCreateDTO)
        {
            return ValidateSubject(subjectCreateDTO.Name, subjectCreateDTO.Credits, subjectCreateDTO.Sphere);
        }

        public static List<ValidationError> Validate(this SubjectUpdateDTO subjectUpdateDTO)
        {
            return ValidateSubject(subjectUpdateDTO.Name, subjectUpdateDTO.Credits, subjectUpdateDTO.Sphere);
        }

        public static List<ValidationError> ValidateSubject(string name, double credits, ClassManager.Common.Enums.SubjectSphere? sphere)
        {
            var errors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(new ValidationError("Subject name can't be empty.", nameof(SubjectCreateDTO.Name)));
            }
            else if (name.Trim().Length < 2)
            {
                errors.Add(new ValidationError("Subject name must be at least 2 characters long.", nameof(SubjectCreateDTO.Name)));
            }

            if (credits <= 0)
            {
                errors.Add(new ValidationError("Credits must be greater than 0.", nameof(SubjectCreateDTO.Credits)));
            }

            if (sphere == null)
            {
                errors.Add(new ValidationError("Subject sphere must be selected.", nameof(SubjectCreateDTO.Sphere)));
            }

            return errors;
        }

        public static List<ValidationError> Validate(this LessonUpdateDTO lessonUpdateDTO)
        {
            return ValidateLesson(lessonUpdateDTO.Topic, lessonUpdateDTO.Type, lessonUpdateDTO.Date, lessonUpdateDTO.StartTime, lessonUpdateDTO.EndTime, false);
        }
    }
}