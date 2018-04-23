
namespace LearningSystem.Services.Implementations
{
    using LearningSystem.Data;
    using LearningSystem.Services.Models.Courses;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TrainerService : ITrainerService
    {
        private readonly LearningSystemDbContext db;

        public TrainerService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CourseListingServiceModel> Courses(string trainerId)
        {
            return this.db.Courses
                .Where(c => c.TrainerId == trainerId)
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
