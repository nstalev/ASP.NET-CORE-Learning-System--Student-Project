
namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<StudentCourse> Courses { get; set; } = new List<StudentCourse>();

        public ICollection<Course> Trainings { get; set; } = new List<Course>();

        public ICollection<Article> Articles { get; set; } = new List<Article>();

    }
}
