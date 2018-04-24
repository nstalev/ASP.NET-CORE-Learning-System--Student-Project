
namespace LearningSystem.Services
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models.Courses;
    using LearningSystem.Services.Models.Students;
    using System.Collections.Generic;

    public interface ITrainerService
    {

        IEnumerable<CourseListingServiceModel> Courses(string trainerId);

        bool IsTrainer(int courseId, string trainerId);

        IEnumerable<StudentInCourseServiceModel> Students(int courseId);


        bool GradeStudent(string studentId, int courseId, Grade grade);
    }
}
