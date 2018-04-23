
namespace LearningSystem.Services
{
    using LearningSystem.Services.Models.Courses;
    using System.Collections.Generic;

    public interface ITrainerService
    {

        IEnumerable<CourseListingServiceModel> Courses(string trainerId);
    }
}
