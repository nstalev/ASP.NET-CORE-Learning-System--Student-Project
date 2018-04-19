
namespace LearningSystem.Services.Models
{
    using System;

    public class CourseListingServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
