

namespace LearningSystem.Services.Admin
{
    using LearningSystem.Services.Admin.Models;
    using System.Collections.Generic;

    public interface IAdminUserService
    {
         IEnumerable<AdminUserListingServiceModel> AllUsers();
    }
}
