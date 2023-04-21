using Concessionaria.Models;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var IsDevelopment = Environment
                    .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

var connectionString = IsDevelopment ?
      builder.Configuration.GetConnectionString("DefaultConnection") :
GetConnectionString();

builder.Services.AddDbContext<ConcessionariaDbContext>(options =>
        options.UseNpgsql(connectionString)
);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static string GetConnectionString()
{
    string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    var databaseUri = new Uri(connectionUrl);

    string db = databaseUri.LocalPath.TrimStart('/');

    string[] userInfo = databaseUri.UserInfo
                        .Split(':', StringSplitOptions.RemoveEmptyEntries);

    return $"User ID=concessionaria;Password=123456;Host=localhost;" +
           $"Database=ConcessionariaDb;Pooling=true;" +
           $"SSL Mode=Require;Trust Server Certificate=True;";
}