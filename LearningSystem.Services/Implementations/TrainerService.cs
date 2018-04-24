
namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using LearningSystem.Services.Models.Courses;
    using LearningSystem.Services.Models.Students;
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
                .ProjectTo<CourseListingServiceModel>()
                .ToList();
        }

        public bool IsTrainer(int courseId, string trainerId)
        {
            return this.db.Courses
                .Any(c => c.Id == courseId && c.TrainerId == trainerId);
        }

        public IEnumerable<StudentInCourseServiceModel> Students(int courseId)
        {
            return this.db.Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c.Students.Select(sc => sc.Student))
                .ProjectTo<StudentInCourseServiceModel>(new { courseId})
                .ToList();
        }
    }
}
