
namespace LearningSystem.Services
{
    using LearningSystem.Services.Models;
    using System.Collections.Generic;

    public interface ICourseService
    {
        IEnumerable<CourseListingServiceModel> ActiveCourses();
    }
}
