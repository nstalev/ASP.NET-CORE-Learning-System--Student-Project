

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using LearningSystem.Services.Admin;
    using LearningSystem.Web.Infrastructure;
    using LearningSystem.Web.Areas.Admin.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using LearningSystem.Data.Models;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;

    [Area("Admin")]
    [Authorize(Roles = WebConstants.AdministratorRole)]
    public class CoursesController : Controller
    {

        private readonly IAdminCourseService coursesService;
        private readonly UserManager<User> userManager;

        public CoursesController(IAdminCourseService coursesService,
                                 UserManager<User> userManager)
        {
            this.coursesService = coursesService;
            this.userManager = userManager;
        }


        public async Task<IActionResult> Create()
        {

            return View(new AddCourseFormModel
            {
                Trainers = await GetTrainers(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(30)
        });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCourseFormModel courseModel)
        {
            if (!ModelState.IsValid)
            {
                courseModel.Trainers = await GetTrainers();
                return View(courseModel);
            }

            this.coursesService.Create(
                courseModel.Name,
                courseModel.Description,
                courseModel.StartDate,
                courseModel.EndDate,
                courseModel.TrainerId);

            TempData.AddSuccessMessage($"Course {courseModel.Name} has been created");

            return RedirectToAction("Index", "Home", new { area = string.Empty });
        }



        private async Task<IEnumerable<SelectListItem>> GetTrainers()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(WebConstants.TrainerRole);

            var trainersList =  trainers
                .Select(t => new SelectListItem
                {
                    Text = t.UserName,
                    Value = t.Id
                })
                .ToList();

            return trainersList;
        }
    }
}