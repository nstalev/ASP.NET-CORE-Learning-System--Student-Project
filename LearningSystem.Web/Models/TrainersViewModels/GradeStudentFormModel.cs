
namespace LearningSystem.Web.Models.TrainersViewModels
{
    using LearningSystem.Data.Models;
    using System.ComponentModel.DataAnnotations;

    public class GradeStudentFormModel
    {

        [Required]
        public string StudentId { get; set; }

        [Required]
        public Grade Grade { get; set; }
    }
}
