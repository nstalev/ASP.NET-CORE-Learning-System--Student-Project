

namespace LearningSystem.Services.Implementations
{
    using System.Collections.Generic;
    using LearningSystem.Data;
    using LearningSystem.Services.Models;
    using System.Linq;
    using System;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;


        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }


        public IEnumerable<CourseListingServiceModel> ActiveCourses()
        {
            return this.db.Courses
                .OrderByDescending(c => c.Id)
                .Where(c => c.StartDate >= DateTime.UtcNow)
                .Select(c => new CourseListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate
                })
                .ToList();
        }
    }
}
