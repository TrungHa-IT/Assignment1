using ASS1.Models;
using ASS1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASS1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetAllCategories();
            return View(categories);
        }

        public async Task<IActionResult> Details(short id)
        {
            var category = await _categoryServices.GetCategoryById(id);
            return View(category);
        }

        public async Task<IActionResult> Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _categoryServices.AddCategory(category);
            return RedirectToAction("Index");
        }
    }
}
