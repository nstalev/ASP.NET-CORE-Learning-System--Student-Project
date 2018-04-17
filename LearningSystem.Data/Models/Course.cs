namespace LearningSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10000)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }


        public IEnumerable<StudentCourse> Students { get; set; } = new List<StudentCourse>();

        public string TrainerId { get; set; }

        public User Trainer { get; set; }
    }
}
