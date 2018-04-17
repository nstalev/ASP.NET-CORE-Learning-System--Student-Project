using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningSystem.Services.Admin;
using LearningSystem.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =WebConstants.AdministratorRole)]
    public class UsersController : Controller
    {
        private readonly IAdminUserService users;


        public UsersController(IAdminUserService users)
        {
            this.users = users;
        }


        public IActionResult Index()
        {

            var allUsers = this.users.AllUsers();

            return View(allUsers);
        }
    }
}