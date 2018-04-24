
namespace LearningSystem.Web.Models.TrainersViewModels
{
    using LearningSystem.Services.Models.Courses;
    using LearningSystem.Services.Models.Students;
    using System.Collections.Generic;

    public class StudentsInCourseViewModel
    {
        public IEnumerable<StudentInCourseServiceModel> Students { get; set; }

        public CourseListingServiceModel Course { get; set; }
    }
}
