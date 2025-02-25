using Microsoft.AspNetCore.Mvc;

namespace ASS1.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
