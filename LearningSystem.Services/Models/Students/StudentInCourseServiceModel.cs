
namespace LearningSystem.Services.Models.Students
{
    using LearningSystem.Data.Models;

    public class StudentInCourseServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Grade? Grade { get; set; }

    }
}
