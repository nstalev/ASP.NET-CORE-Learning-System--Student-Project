
namespace LearningSystem.Services
{
    using LearningSystem.Services.Models.Courses;
    using System.Collections.Generic;

    public interface ICourseService
    {
        IEnumerable<CourseListingServiceModel> ActiveCourses();

        CourseDetailsServiceModel ById(int id);

        CourseListingServiceModel CourseBasicById(int courseId);

        bool StudentIsEnrolledInCourse(string studentId, int courseId);

        bool SignUpStudent(int courseId, string studentId);

        bool SignOutStudent(int courseId, string studentId);

        bool SaveExamSubmission(int id, string studentId, byte[] fileContent);
    }
}
