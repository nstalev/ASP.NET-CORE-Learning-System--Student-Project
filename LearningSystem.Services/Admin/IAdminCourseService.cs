
using System;

namespace LearningSystem.Services.Admin
{
    public interface IAdminCourseService
    {
        void Create(string name, string description, DateTime startDate, DateTime endDate, string trainerId);
    }
}
