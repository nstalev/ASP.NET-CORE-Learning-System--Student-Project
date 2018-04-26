
using AutoMapper.QueryableExtensions;
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

        public IEnumerable<ArticleListingServiceModel> AllArticles(string search, int page)
        {
            return this.db.Articles
                .OrderByDescending(a => a.PublishDate)
                .Where(a => a.Title.ToLower().Contains(search.ToLower())
                || a.Content.ToLower().Contains(search.ToLower()))
                .Skip((page - 1) * ServiceConstants.ArticlesListingPageSize)
                .Take(ServiceConstants.ArticlesListingPageSize)
                .ProjectTo<ArticleListingServiceModel>()
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

        public int Total(string search)
        {
            return this.db.Articles
                 .Where(a => a.Title.ToLower().Contains(search.ToLower())
                || a.Content.ToLower().Contains(search.ToLower()))
                .Count();
        }



    }
}
