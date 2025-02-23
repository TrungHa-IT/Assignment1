using ASS1.DAO;
using ASS1.Models;
using ASS1.Services;
using ASS1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASS1.Controllers
{
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleServices _newsArticleServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ITagServices _tagServices;
        public NewsArticleController(INewsArticleServices newsArticleServices, ICategoryServices categoryServices, ITagServices tagServices)
        {
            _newsArticleServices = newsArticleServices;
            _categoryServices = categoryServices;
            _tagServices = tagServices;
        }
        public async Task<IActionResult> Index()
        {
            var articles = await _newsArticleServices.GetAllNews();
            return View(articles);
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
            var tags = await _tagServices.GetAllTags(); // Đợi dữ liệu trả về
            var categories = await _categoryServices.GetAllCategories();

            var model = new NewsArticleViewModel
            {
                Categories = categories.ToList(), // Chuyển về List<Category>
                Tags = tags.ToList() // Chuyển về List<Tag>
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewsArticleViewModel model)
        {
           

            // Tạo đối tượng NewsArticle
            var newsArticle = new NewsArticle
            {
                NewsArticleId = "SA1",
                NewsTitle = model.NewsTitle?.Trim() ?? "Untitled",
                Headline = model.Headline?.Trim() ?? "No Headline",
                CreatedDate = DateTime.UtcNow, // Sử dụng UTC để đồng bộ thời gian
                NewsContent = model.NewsContent?.Trim() ?? "",
                CategoryId = (short)(model.SelectedTagCategoryID ?? 0),
                ModifiedDate = DateTime.UtcNow,
                CreatedById = 1,
                UpdatedById = 1,
            };

            // Gọi service để thêm bài viết kèm Tags
            await _newsArticleServices.AddNewsArticleWithTagsAsync(newsArticle, model.SelectedTagIds ?? new List<int>());

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var article = await _newsArticleServices.GetNewsById(id);
            if (article == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(await _categoryServices.GetAllCategories(), "CategoryId", "CategoryName");
            ViewBag.Tags = new MultiSelectList(await _tagServices.GetAllTags(), "TagId", "TagName", article.Tags.Select(t => t.TagId));

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsArticle model, List<int> selectedTags)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryServices.GetAllCategories(), "CategoryId", "CategoryName");
                //ViewBag.Tags = new MultiSelectList(await _tagServices.GetAllTags(), "TagId", "TagName", article.Tags.Select(t => t.TagId));

                return View(model);
            }

            await _newsArticleServices.UpdateNews(model);
            return RedirectToAction("Index");
        }


    }
}
