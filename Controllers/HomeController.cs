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

        // Tìm người dùng theo email
        var user = await _funewsManagementContext.SystemAccounts
            .FirstOrDefaultAsync(u => u.AccountEmail == loginVM.AccountEmail);

        // Kiểm tra tài khoản và mật khẩu
        if (user == null || user.AccountPassword != loginVM.AccountPassword)
        {
            ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
            return View(loginVM);
        }

        // Chuyển AccountRole từ số sang tên vai trò (ví dụ: 1 -> "Staff", 2 -> "Lecturer", 3 -> "Admin")
        string userRole = user.AccountRole switch
        {
            1 => "Staff",
            2 => "Lecturer",
            3 => "Admin",
            _ => "User" // Nếu không có role nào phù hợp
        };

        // Tạo danh sách claims từ thông tin tài khoản
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.AccountName ?? ""),
        new Claim(ClaimTypes.Email, user.AccountEmail ?? ""),
        new Claim(ClaimTypes.Role, userRole), // Lưu role dưới dạng chuỗi (Staff, Lecturer, Admin)
        new Claim("AccountId", user.AccountId.ToString())
    };

        // Tạo danh tính claims
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        // Đăng nhập với cookie
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

        // Điều hướng người dùng dựa trên claim role
        if (User.IsInRole("Lecturer"))
        {
            return RedirectToAction("Lecturer", "NewsArticle");
        }
        if (User.IsInRole("Staff"))
        {
            return RedirectToAction("Index", "Staff");
        }
        if (User.IsInRole("Admin"))
        {
            return RedirectToAction("Index", "Admin");
        }

        // Trường hợp mặc định
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
