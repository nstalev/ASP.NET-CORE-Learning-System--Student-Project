
namespace LearningSystem.Web.Infrastructure.Mapping
{
    using AutoMapper;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models.Courses;
    using LearningSystem.Services.Models.Students;
    using System.Linq;

    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            this.CreateMap<Course, CourseListingServiceModel>();

            this.CreateMap<Course, CourseDetailsServiceModel>()
                .ForMember(c => c.Trainer, cfg => cfg.MapFrom(c => c.Trainer.Name))
                .ForMember(c => c.Students, cfg => cfg.MapFrom(c => c.Students.Count()));

            int courseId = default(int);
            this.CreateMap<User, StudentInCourseServiceModel>()
                .ForMember(s => s.Grade, cfg => cfg.MapFrom(u => u.Courses
                    .Where(c => c.CourseId == courseId)
                    .Select(c => c.Grade)
                    .FirstOrDefault()));

        }
    }
}
