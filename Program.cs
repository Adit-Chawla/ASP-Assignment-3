
using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add MySQL
var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseMySQL(connectionString));

//Enabling Sessions
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; 
});

var app = builder.Build();

// Turn On Sessions
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "Brief",
    pattern: "Brief",
    defaults: new { controller="Home", action = "Brief"}
);

app.MapControllerRoute(
    name: "Privacy",
    pattern: "Privacy",
    defaults: new { controller="Home", action = "Privacy"}
);

app.MapControllerRoute(
    name: "MyCart",
    pattern: "MyCart",
    defaults: new { controller="Carts", action = "MyCart"} 
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();