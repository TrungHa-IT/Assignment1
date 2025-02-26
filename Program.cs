using ASS1.DAO;
using ASS1.Models;
using ASS1.Repositories;
using ASS1.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FunewsManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelDb")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Đường dẫn khi chưa đăng nhập
        options.AccessDeniedPath = "/Account/AccessDenied"; // Khi không có quyền
    });

builder.Services.AddAuthorization();


// Register DAO first
builder.Services.AddScoped<ASS1.DAO.NewsArticleDAO>();

// Register Repository
builder.Services.AddScoped<ASS1.Repositories.INewsArticleRepository, ASS1.Repositories.NewsArticleRepository>();

// Register Services
builder.Services.AddSession(); // Đăng ký dịch vụ Session
builder.Services.AddSingleton<EmailService>();
builder.Services.AddScoped<ITagDAO, TagDAO>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagServices, TagServices>();

builder.Services.AddScoped<ISystemAccountDAO, SystemAccountDAO>();
builder.Services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
builder.Services.AddScoped<ISystemAccountServices, SystemAccountServices>();

builder.Services.AddScoped<ICategoryDAO, CategoryDAO>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();

builder.Services.AddScoped<INewsArticleDAO, NewsArticleDAO>();
builder.Services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
builder.Services.AddScoped<INewsArticleServices, NewsArticleServices>();


var app = builder.Build();

app.UseSession();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization(); 



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
