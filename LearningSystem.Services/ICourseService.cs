
namespace LearningSystem.Services
{
    using LearningSystem.Services.Models.Courses;
    using System.Collections.Generic;

    public interface ICourseService
    {
        IEnumerable<CourseListingServiceModel> ActiveCourses();

        CourseDetailsServiceModel ById(int id);
        bool StudentIsSignedUpToCourse(string studentId, int courseId);

        bool SignUpStudent(int courseId, string studentId);
    }
}
