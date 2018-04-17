using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningSystem.Data.Models;
using LearningSystem.Services.Admin;
using LearningSystem.Web.Areas.Admin.Models;
using LearningSystem.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =WebConstants.AdministratorRole)]
    public class UsersController : Controller
    {
        private readonly IAdminUserService users;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public UsersController(IAdminUserService users,
                                UserManager<User> userManager,
                                RoleManager<IdentityRole> roleManager)
        {
            this.users = users;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var allUsers = this.users.AllUsers();

            return View(allUsers);
        }


        public async Task<IActionResult> Details(string id)
        {

            var user = await  this.userManager.FindByIdAsync(id);

            var userRoles = await this.userManager.GetRolesAsync(user);

            var allRoles = this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View( new AdminUserDetailsViewModel
            {
                Id = user.Id,
                UserName = user.Name,
                Email  = user.Email,
                Name = user.Email,
                UserRoles = userRoles,
                Roles = allRoles
            });
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string UserId, string role)
        {
            var user = await this.userManager.FindByIdAsync(UserId);

            var roleExists = await this.roleManager.RoleExistsAsync(role);

            if (user == null || !roleExists)
            {
                return NotFound();
            }

            await this.userManager.AddToRoleAsync(user, role);


            return RedirectToAction("Details", new { id = UserId });
        }
    }
}