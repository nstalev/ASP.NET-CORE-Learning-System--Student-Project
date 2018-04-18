
namespace LearningSystem.Web.Areas.Blog.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PublishArticleFormModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }


    }
}
