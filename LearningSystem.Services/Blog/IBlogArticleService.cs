
namespace LearningSystem.Services.Blog
{
    using System;
    using System.Collections.Generic;
    using Models;

    public interface IBlogArticleService
    {
        void Create(string title, string content, DateTime publishDate, string userId);

        IEnumerable<ArticleListingServiceModel> AllArticles(int page);

        int Total();
    }
}
