using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningSystem.Data.Models;
using LearningSystem.Services;
using LearningSystem.Web.Infrastructure;
using LearningSystem.Web.Models.TrainersViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{

    [Authorize(Roles =WebConstants.TrainerRole)]
    public class TrainersController : Controller
    {

        private readonly ITrainerService trainersService;
        private readonly ICourseService coursesService;
        private readonly UserManager<User> userManager;

        public TrainersController(ITrainerService trainersService,
                                  ICourseService coursesService,
                                    UserManager<User> userManager)
        {
            this.trainersService = trainersService;
            this.userManager = userManager;
            this.coursesService = coursesService;
        }


        public IActionResult Courses()
        {
            var trainerId = this.userManager.GetUserId(User);

            var courses = this.trainersService.Courses(trainerId);

            return View(courses);
        }

        public IActionResult Students(int courseId)
        {
            var trainerId = this.userManager.GetUserId(User);

            if (!this.trainersService.IsTrainer(courseId, trainerId))
            {
                return NotFound();
            }

            var students = this.trainersService.Students(courseId);

            return View(new StudentsInCourseViewModel
            {
                Students = students,
                Course = this.coursesService.CourseBasicById(courseId)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GradeStudent(int courseId, GradeStudentFormModel gradeModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Students));
            }

            var trainerId = this.userManager.GetUserId(User);

            if (!this.trainersService.IsTrainer(courseId, trainerId))
            {
                return NotFound();
            }

            var success = this.trainersService.GradeStudent(
                  gradeModel.StudentId,
                  courseId,
                  gradeModel.Grade
                );



            if (!success)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(Students), new { courseId });
        }


        public IActionResult DownloadExam(int courseId, string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                return NotFound();

            }

            var trainerId = this.userManager.GetUserId(User);

            if (!this.trainersService.IsTrainer(courseId, trainerId))
            {
                return NotFound();
            }

            var examContent = this.trainersService.GetExamSubmission(courseId, studentId);

            if (examContent == null)
            {
                return BadRequest();
            }

            var StudentInCourseName = this.trainersService.GetStudentInCourseName(courseId, studentId);

            if (StudentInCourseName == null)
            {
                return BadRequest();
            }

            return File(examContent, "application/zip", $"{StudentInCourseName.CourseName} - {StudentInCourseName.UserName}.zip");
                
        }
    }
}