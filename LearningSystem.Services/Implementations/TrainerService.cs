
namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
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

        public bool GradeStudent(string studentId, int courseId, Grade grade)
        {
            var studentCourse = this.db.Find<StudentCourse>(studentId, courseId);

            if (studentCourse == null)
            {
                return false;
            }

            studentCourse.Grade = grade;
            this.db.SaveChanges();

            return true;
        }

        public byte[] GetExamSubmission(int courseId, string studentid)
        {
            var studentCourse = this.db.Find<StudentCourse>(studentid, courseId);

            if (studentCourse == null)
            {
                return null;
            }

            return studentCourse.ExamSubmission;
        }

        public StudentInCourseNameServiceModel GetStudentInCourseName(int courseId, string studentid)
        {
            var userName = this.db.Users
                .Where(u => u.Id == studentid)
                .Select(s => s.UserName)
                .FirstOrDefault();

            var courseName = this.db.Courses
                .Where(c => c.Id == courseId)
                .Select(c => c.Name)
                .FirstOrDefault();

            return new StudentInCourseNameServiceModel
            {
                UserName = userName,
                CourseName = courseName
            };
        }
    }
}
