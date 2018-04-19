

namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using System;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Blog;
    using LearningSystem.Services.Html;
    using LearningSystem.Web.Areas.Blog.Models;
    using LearningSystem.Web.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Blog")]
    public class ArticlesController : Controller
    {
        private readonly IHtmlService htmlService;
        private readonly IBlogArticleService articlesService;
        private readonly UserManager<User> userManager;

        public ArticlesController(IHtmlService htmlService,
                                  IBlogArticleService articlesService,
                                  UserManager<User> userManager)
        {
            this.htmlService = htmlService;
            this.articlesService = articlesService;
            this.userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            var articleListing = this.articlesService.AllArticles(page);

            int TotalArticles = this.articlesService.Total();


            return View(new ArticleListingViewModel
            {
                Articles = articleListing,
                CurrentPage = page,
                TotalArticles = TotalArticles
            });
        }


        [Authorize(Roles  = WebConstants.BlogAuthorRole)]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = WebConstants.BlogAuthorRole)]
        [HttpPost]
        public IActionResult Create(PublishArticleFormModel articleModel)
        {

            if (!ModelState.IsValid)
            {
                return View(articleModel);
            }

            var userId = this.userManager.GetUserId(User);

            var content = this.htmlService.Sanitize(articleModel.Content);

            var publishDate = DateTime.UtcNow;

            this.articlesService.Create(articleModel.Title, content, publishDate, userId);

            TempData.AddSuccessMessage($"Article {articleModel.Title} successfully has been published");

            return RedirectToAction(nameof(Index));
        }


    }
}