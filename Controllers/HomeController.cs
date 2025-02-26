using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ASS1.Models;
using ASS1.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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

        var user = await _funewsManagementContext.SystemAccounts
            .FirstOrDefaultAsync(u => u.AccountEmail == loginVM.AccountEmail);

        if (user == null || user.AccountPassword != loginVM.AccountPassword)
        {
            ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
            return View(loginVM);
        }

        string userRole = user.AccountRole switch
        {
            1 => "Staff",
            2 => "Lecturer",
            3 => "Admin",
            _ => "User"
        };

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.AccountName ?? ""),
        new Claim(ClaimTypes.Email, user.AccountEmail ?? ""),
        new Claim(ClaimTypes.Role, userRole),
        new Claim("AccountId", user.AccountId.ToString())
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        return userRole switch
        {
            "Lecturer" => RedirectToAction("Lecturer", "NewsArticle"),
            "Staff" => RedirectToAction("Index", "Staff"),
            "Admin" => RedirectToAction("Index", "Admin"),
            _ => RedirectToAction("Login", "Home")
        };
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
