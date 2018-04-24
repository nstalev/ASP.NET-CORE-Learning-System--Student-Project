using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningSystem.Data.Models;
using LearningSystem.Services;
using LearningSystem.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Controllers
{

    [Authorize(Roles =WebConstants.TrainerRole)]
    public class TrainersController : Controller
    {

        private readonly ITrainerService trainersService;
        private readonly UserManager<User> userManager;

        public TrainersController(ITrainerService trainersService,
                                    UserManager<User> userManager)
        {
            this.trainersService = trainersService;
            this.userManager = userManager;
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

            return View(students);
        }
    }
}