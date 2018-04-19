
using LearningSystem.Data;
using LearningSystem.Data.Models;
using LearningSystem.Services.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LearningSystem.Services.Blog.Implementations
{
    public class BlogArticleService : IBlogArticleService
    {
        private readonly LearningSystemDbContext db;

        public BlogArticleService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ArticleListingServiceModel> AllArticles(int page)
        {
            return this.db.Articles
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * ServiceConstants.ArticlesListingPageSize)
                .Take(ServiceConstants.ArticlesListingPageSize)
                .Select(a => new ArticleListingServiceModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Author = a.Author.Name,
                    PublishDate = a.PublishDate
                    
                })
                .ToList();
        }

        public DetailsArticleServiceModel ById(int id)
        {
            return this.db.Articles
                .Where(a => a.Id == id)
                .Select(a => new DetailsArticleServiceModel
                {
                     Title = a.Title,
                     Content = a.Content,
                     Author = a.Author.Name,
                     PublishDate = a.PublishDate
                })
                .FirstOrDefault();

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

        public int Total()
        {
            return this.db.Articles.Count();
        }
    }
}
