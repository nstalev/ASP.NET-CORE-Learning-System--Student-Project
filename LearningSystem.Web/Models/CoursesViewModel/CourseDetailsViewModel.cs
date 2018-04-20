

namespace LearningSystem.Web.Models.CoursesViewModel
{
    using LearningSystem.Services.Models.Courses;

    public class CourseDetailsViewModel
    {
        public CourseDetailsServiceModel Course { get; set; }

        public bool IsSutentSignedUp { get; set; }

    }
}
