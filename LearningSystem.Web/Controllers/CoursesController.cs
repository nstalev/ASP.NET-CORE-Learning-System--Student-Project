


namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services;
    using LearningSystem.Web.Models.CoursesViewModel;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using LearningSystem.Web.Infrastructure;
    public class CoursesController : Controller
    {
        private readonly ICourseService coursesService;
        private readonly UserManager<User> userManager;

        public CoursesController(ICourseService coursesService,
                                UserManager<User> userManager)
        {
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public IActionResult Details(int id)
        {


            var vm = new CourseDetailsViewModel
            {
                Course = this.coursesService.ById(id)
            };

            if (User.Identity.IsAuthenticated)
            {
                var studentId = this.userManager.GetUserId(User);

                var StudentIsSignedUp = this.coursesService.StudentIsSignedUpToCourse(studentId, id);

                vm.IsSutentSignedUp = StudentIsSignedUp;
            }

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult SignUp(int id)
        {
            var studentId = this.userManager.GetUserId(User);

            var succes = this.coursesService.SignUpStudent(id, studentId);

            if (!succes)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Thank you for your Signed Up");

            return RedirectToAction(nameof(Details), new { id });
        }


        [HttpPost]
        [Authorize]
        public IActionResult SignOut(int id)
        {
            var studentId = this.userManager.GetUserId(User);

            var succes = this.coursesService.SignOutStudent(id, studentId);

            if (!succes)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("You successfully signed out from this course");

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}