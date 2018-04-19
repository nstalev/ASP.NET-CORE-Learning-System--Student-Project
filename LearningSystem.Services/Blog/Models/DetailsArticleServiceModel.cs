
namespace LearningSystem.Services.Blog.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DetailsArticleServiceModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        public string Author { get; set; }
    }
}
