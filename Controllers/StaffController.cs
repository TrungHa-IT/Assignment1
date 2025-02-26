using ASS1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASS1.Controllers
{
    public class StaffController : Controller
    {
        private readonly INewsArticleServices _newsArticleServices;
        private readonly ISystemAccountServices _systemAccountServices;
        public StaffController(INewsArticleServices newsArticleServices, ISystemAccountServices systemAccountServices)
        {
            _newsArticleServices = newsArticleServices;
            _systemAccountServices = systemAccountServices;
        }
        [Authorize(Roles = "Staff")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize (Roles ="Staff")]
        public async Task<IActionResult> History()
        {
            var userIDString = User.FindFirst("AccountId")?.Value;
            short? userID = null;

            if (short.TryParse(userIDString, out short parsedUserID))
            {
                userID = parsedUserID;
            }

            var news = await _newsArticleServices.GetNewsByAccountID(userID ?? 0); // ✅ Add await
            return View(news); // ✅ Convert to List
        }
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Profile()
        {
            var userIDString = User.FindFirst("AccountId")?.Value;
            short? userID = null;

            if (short.TryParse(userIDString, out short parsedUserID))
            {
                userID = parsedUserID;
            }
            var userDetails = await _systemAccountServices.GetAccountById(userID ?? 0); // ✅ Add await
            return View(userDetails);
        }

        
    }
}
