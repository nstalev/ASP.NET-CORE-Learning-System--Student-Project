
namespace LearningSystem.Web.Areas.Admin.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class AddCourseFormModel : IValidatableObject
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10000)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name ="Trainers")]
        [Required]
        public string TrainerId { get; set; }

        public IEnumerable<SelectListItem> Trainers { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate < DateTime.UtcNow.Date)
            {
                yield return new ValidationResult("Start date cannot be in the past.");
            }

            if (this.StartDate > this.EndDate)
            {
                yield return new ValidationResult("Start date should be before end date.");
            }
        }


    }
}
