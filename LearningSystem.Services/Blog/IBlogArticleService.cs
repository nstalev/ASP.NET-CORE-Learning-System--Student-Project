
using System;

namespace LearningSystem.Services.Blog
{
    public interface IBlogArticleService
    {
        void Create(string title, string content, DateTime publishDate, string userId);
    }
}
