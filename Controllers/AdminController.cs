using ASS1.Services;
using ASS1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASS1.Controllers
{
    public class AdminController : Controller
    {
        private readonly INewsArticleServices _newsArticleServices;
        public AdminController(INewsArticleServices newsArticleServices)
        {
            _newsArticleServices = newsArticleServices;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async  Task<IActionResult> Dashboard(DateTime startDate, DateTime endDate)
        {
            var reportData = await _newsArticleServices.GetNewsByDateRange(startDate, endDate);
            var viewModel = new NewsReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                NewsList = reportData.ToList() // Chuyển IEnumerable thành List
            };
            return View(viewModel);
        }
    }
}
