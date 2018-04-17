
using System.Collections.Generic;
using System.Linq;
using LearningSystem.Data;
using LearningSystem.Services.Admin.Models;

namespace LearningSystem.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly LearningSystemDbContext db;


        public AdminUserService(LearningSystemDbContext db)
        {
            this.db = db;
        }


        public IEnumerable<AdminUserListingServiceModel> AllUsers()
        {

            return this.db.Users
                .Select(u => new AdminUserListingServiceModel
                {
                     Id = u.Id,
                     Name= u.Name,
                     UserName = u.UserName
                })
                .ToList();

        }
    }
}
