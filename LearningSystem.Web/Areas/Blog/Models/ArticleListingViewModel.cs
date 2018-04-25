﻿
namespace LearningSystem.Web.Areas.Blog.Models
{
    using LearningSystem.Services;
    using LearningSystem.Services.Blog.Models;
    using System;
    using System.Collections.Generic;

    public class ArticleListingViewModel
    {
        public IEnumerable<ArticleListingServiceModel> Articles { get; set; }

        public int CurrentPage { get; set; }

        public string Search { get; set; } 

        public int TotalArticles { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalArticles / ServiceConstants.ArticlesListingPageSize);

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;


    }
}
