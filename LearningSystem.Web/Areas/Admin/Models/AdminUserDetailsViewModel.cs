
namespace LearningSystem.Web.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class AdminUserDetailsViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> UserRoles { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
