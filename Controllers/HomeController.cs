using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASS1.Models;
using ASS1.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace ASS1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly FunewsManagementContext _funewsManagementContext;

    public HomeController(ILogger<HomeController> logger, FunewsManagementContext funewsManagementContext)
    {
        _logger = logger;
        _funewsManagementContext = funewsManagementContext;
    }

    public IActionResult Index()
    {
        return View();

    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVM);
        }

        // Tìm người dùng theo email
        var user = await _funewsManagementContext.SystemAccounts
     .FirstOrDefaultAsync(u => u.AccountEmail == loginVM.AccountEmail);


        if (user == null || user.AccountPassword != loginVM.AccountPassword)
        {
            ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
            return View(loginVM);
        }

        // Lưu thông tin vào Session (hoặc Cookie)
        HttpContext.Session.SetString("UserEmail", user.AccountEmail);
        if (user.AccountRole == 2)
        {
            return RedirectToAction("Index", "Tags");
        }
        else if (user.AccountRole == 1)
        {
            return RedirectToAction("Index", "NewsArticle");
        }
        else if(user.AccountRole == 3)
        {
            return RedirectToAction("Index", "Category");
        }
            // Chuyển hướng đến trang chính sau khi đăng nhập
        return RedirectToAction("Login", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        if (!ModelState.IsValid)
        {
            return View(registerVM);
        }
        int lastId = _funewsManagementContext.SystemAccounts
                .OrderByDescending(a => a.AccountId)
                .Select(a => a.AccountId)
                .FirstOrDefault();
        int newId = lastId + 1;

        SystemAccount s = new SystemAccount()
        {
            AccountId = (short) newId,
            AccountName = registerVM.AccountName,
            AccountEmail = registerVM.AccountEmail,
            AccountPassword = registerVM.Password,
            AccountRole = 1
        };

        _funewsManagementContext.AddAsync(s);
        _funewsManagementContext.SaveChanges();
        return RedirectToAction("Index", "NewsArticle");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
