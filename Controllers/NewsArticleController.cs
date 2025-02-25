using ASS1.DAO;
using ASS1.Models;
using ASS1.Services;
using ASS1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASS1.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleServices _newsArticleServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ITagServices _tagServices;
        private readonly EmailService _emailService;
        
        public NewsArticleController(INewsArticleServices newsArticleServices, ICategoryServices categoryServices, ITagServices tagServices, EmailService emailService)
        {
            _newsArticleServices = newsArticleServices;
            _categoryServices = categoryServices;
            _tagServices = tagServices;
            _emailService = emailService;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var articles = await _newsArticleServices.GetAllNews(); 
            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(s => s.NewsTitle.Contains(searchString)
                                       || s.NewsContent.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Date":
                    articles = articles.OrderBy(s => s.CreatedDate);
                    break;
                case "status":
                    articles = articles.OrderByDescending(s => s.ModifiedDate);
                    break;
                default:
                    
                    break;
            }
            int pageSize = 8;
            return View(await PaginatedList<NewsArticle>.CreateAsync(articles, pageNumber ?? 1, pageSize));

        }

        public async Task<IActionResult> Lecturer()
        {
            var articles = await _newsArticleServices.GetAllNewsStatus();
            return View(articles);
        }
        public async Task<IActionResult> LecturerDetails(string id)
        {
            var category = await _newsArticleServices.GetNewsById(id);
            return View(category);
        }

        public async Task<IActionResult> Details(string id)
        {
            var category = await _newsArticleServices.GetNewsById(id);
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await _newsArticleServices.DeleteNews(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var tags = await _tagServices.GetAllTags();
            var categories = await _categoryServices.GetAllCategories();

            var model = new NewsArticleViewModel
            {
                Categories = categories.ToList(), 
                Tags = tags.ToList() 
            };

            return View(model);
        }

        public string GetRandomArticleID()
        {
            return "SA" + new Random().Next(10, 9999);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsArticleViewModel model)
        {
            if(!ModelState.IsValid)
            {
                var tags = await _tagServices.GetAllTags(); 
                var categories = await _categoryServices.GetAllCategories();

                var modelvm = new NewsArticleViewModel
                {
                    Categories = categories.ToList(), 
                    Tags = tags.ToList() 
                };

                return View(modelvm);
            }

            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userIDString = User.FindFirst("AccountId")?.Value;

            
            short? userID = null;
            if (short.TryParse(userIDString, out short parsedUserID))
            {
                userID = parsedUserID;
            }

            string NewsArticleIdString = GetRandomArticleID();

            var newsArticle = new NewsArticle
            {
                NewsArticleId = NewsArticleIdString,
                NewsTitle = model.NewsTitle?.Trim() ?? "Untitled",
                Headline = model.Headline?.Trim() ?? "No Headline",
                CreatedDate = DateTime.UtcNow,
                NewsContent = model.Headline?.Trim() ?? "No New Content",
                CategoryId = (short)(model.SelectedTagCategoryID ?? 0),
                ModifiedDate = DateTime.UtcNow,
                CreatedById = userID ?? 0, 
                UpdatedById = userID ?? 0,
                NewsStatus = true,
                NewsSource = "N/A"
            };

            await _newsArticleServices.AddNewsArticleWithTagsAsync(newsArticle, model.SelectedTagIds ?? new List<int>());

            string adminEmail = "trunghhhe176079@fpt.edu.vn";
            string subject = "New Article Published";
            string id = model.NewsArticleId;
            string body = $"<h3>{model.NewsTitle}</h3><p>By {userName}</p><a href='http://localhost:5019/NewsArticle/Details/{NewsArticleIdString}'>View Article</a>";

            await _emailService.SendEmailAsync(adminEmail, subject, body);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var articleResult = await _newsArticleServices.GetNewsById(id);
            var tags = await _tagServices.GetAllTags();
            var categories = await _categoryServices.GetAllCategories();

            var model = new NewsArticleViewModel
            {
                NewsArticleId = articleResult.NewsArticleId,
                NewsTitle = articleResult.NewsTitle,
                Headline = articleResult.Headline,
                NewsContent = articleResult.NewsContent,
                NewsSource = articleResult.NewsSource,
                NewsStatus = articleResult.NewsStatus,

                Categories = categories.ToList(),
                Tags = tags.ToList(),

                SelectedTagCategoryID = articleResult.Category?.CategoryId,  
                SelectedTagIds = articleResult.Tags?.Select(t => t.TagId).ToList() ?? new List<int>() 
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsArticleViewModel newsArticleViewModel, string id)
        {
            if (!ModelState.IsValid)
            {
                var articleResult = await _newsArticleServices.GetNewsById(id);
                var tags = await _tagServices.GetAllTags();
                var categories = await _categoryServices.GetAllCategories();

                var model = new NewsArticleViewModel
                {
                    NewsArticleId = articleResult.NewsArticleId,
                    NewsTitle = articleResult.NewsTitle,
                    Headline = articleResult.Headline,
                    NewsContent = articleResult.NewsContent,
                    NewsSource = articleResult.NewsSource,
                    NewsStatus = articleResult.NewsStatus,

                    Categories = categories.ToList(),
                    Tags = tags.ToList(),

                    SelectedTagCategoryID = articleResult.Category?.CategoryId,  
                    SelectedTagIds = articleResult.Tags?.Select(t => t.TagId).ToList() ?? new List<int>() 
                };

                return View(model);
            }

            var userIDString = User.FindFirst("AccountId")?.Value;
            short? userID = null;
            if (short.TryParse(userIDString, out short parsedUserID))
            {
                userID = parsedUserID;
            }
            var existingArticle = await _newsArticleServices.GetNewsById(id);
            if (existingArticle == null)
            {
                return NotFound();
            }

            existingArticle.NewsTitle = newsArticleViewModel.NewsTitle;
            existingArticle.Headline = newsArticleViewModel.Headline;
            existingArticle.NewsContent = newsArticleViewModel.NewsContent;
            existingArticle.CategoryId = (short)(newsArticleViewModel.SelectedTagCategoryID ?? 0);
            existingArticle.NewsStatus = newsArticleViewModel.NewsStatus;
            existingArticle.NewsSource = newsArticleViewModel.NewsSource;
            existingArticle.UpdatedById = userID;
            existingArticle.ModifiedDate = DateTime.UtcNow;

            await _newsArticleServices.UpdateNewsArticleWithTagsAsync(existingArticle, newsArticleViewModel.SelectedTagIds ?? new List<int>());
            return RedirectToAction("Index");

        }

    }
}
