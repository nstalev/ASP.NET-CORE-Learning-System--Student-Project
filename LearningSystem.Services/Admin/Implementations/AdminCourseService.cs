

namespace LearningSystem.Services.Admin.Implementations
{
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using System;

    public class AdminCourseService : IAdminCourseService
    {
        private readonly LearningSystemDbContext db;


        public AdminCourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }


        public void Create(string name, string description, DateTime startDate, DateTime endDate, string trainerId)
        {
            var course = new Course
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                TrainerId = trainerId
            };

            this.db.Courses.Add(course);

            this.db.SaveChanges();

        }
    }
}
