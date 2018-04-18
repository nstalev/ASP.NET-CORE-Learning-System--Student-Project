
using LearningSystem.Data;
using LearningSystem.Data.Models;
using System;

namespace LearningSystem.Services.Blog.Implementations
{
    public class BlogArticleService : IBlogArticleService
    {
        private readonly LearningSystemDbContext db;

        public BlogArticleService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public void Create(string title, string content, DateTime publishDate, string userId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                PublishDate = publishDate,
                AuthorId = userId
            };

            db.Articles.Add(article);
            db.SaveChanges();
        }
    }
}
