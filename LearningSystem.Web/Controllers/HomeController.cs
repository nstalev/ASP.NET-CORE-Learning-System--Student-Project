
namespace LearningSystem.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using LearningSystem.Web.Models;
    using LearningSystem.Services;

    public class HomeController : Controller
    {

        private readonly ICourseService coursesService;

        public HomeController(ICourseService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {

            return View(this.coursesService.ActiveCourses());
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
