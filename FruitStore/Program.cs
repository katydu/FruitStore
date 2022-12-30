// to use UseSqlServer() method
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using FruitStore.Data;
// using cookie authentication
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// new
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();
// new
builder.Services.AddDistributedMemoryCache();
// new
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "cart";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//new
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth";
        //authentication ticket expire time
        options.ExpireTimeSpan = TimeSpan.FromMinutes(100);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });
// new
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
// wwwwroot file
app.UseStaticFiles();

// new
app.UseSession();

app.UseRouting();
// cookie 認證
app.UseAuthentication();
// 授權
app.UseAuthorization();

app.MapRazorPages();

// setting default using which controller and action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

