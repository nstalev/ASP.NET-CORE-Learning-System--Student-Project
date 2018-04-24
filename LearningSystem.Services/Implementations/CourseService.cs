

namespace LearningSystem.Services.Implementations
{
    using System.Collections.Generic;
    using LearningSystem.Data;
    using LearningSystem.Services.Models.Courses;
    using System.Linq;
    using System;
    using LearningSystem.Data.Models;
    using AutoMapper.QueryableExtensions;

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
                .ProjectTo<CourseListingServiceModel>()
                .ToList();
        }

        public CourseDetailsServiceModel ById(int id)
        {

            return this.db.Courses
               .Where(c => c.Id == id)
               .ProjectTo<CourseDetailsServiceModel>()
               .FirstOrDefault();
        }

        public bool StudentIsSignedUpToCourse(string studentId, int courseId)
        {
            return this.db.Courses
                .Any(c => c.Id == courseId
                && c.Students.Any(s => s.StudentId == studentId));
        }

        public bool SignUpStudent(int courseId, string studentId)
        {
            var courseInfo = CourseInfo(courseId, studentId);

            if (courseInfo == null 
                || courseInfo.StartDate < DateTime.UtcNow
                || courseInfo.StudentIsEnrolled)
            {
                return false;
            }

            var studentCourse = new StudentCourse
            {
                CourseId = courseId,
                StudentId = studentId
            };

            this.db.Add(studentCourse);
            this.db.SaveChanges();


            return true;
        }

        public bool SignOutStudent(int courseId, string studentId)
        {
            var courseInfo = CourseInfo(courseId, studentId);

            if (courseInfo == null
               || courseInfo.StartDate < DateTime.UtcNow
               || !courseInfo.StudentIsEnrolled)
            {
                return false;
            }

            var studentCourse = this.db.Courses
                .Where(c => c.Id == courseId)
                .SelectMany(s => s.Students)
                .FirstOrDefault(s => s.StudentId == studentId);

           // var studentCourse = this.db.Find<StudentCourse>(courseId, studentId);

            this.db.Remove(studentCourse);
            this.db.SaveChanges();

            return true;
        }


        private CourseSignUpServiceModel CourseInfo(int courseId, string studentId)
            => this.db.Courses
                    .Where(c => c.Id == courseId)
                    .Select(c => new CourseSignUpServiceModel
                    {
                        StartDate = c.StartDate,
                        StudentIsEnrolled = c.Students.Any(s => s.StudentId == studentId)

                    })
                    .FirstOrDefault();

      
    }
}
