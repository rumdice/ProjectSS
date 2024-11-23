using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using LogApp.Data;
using Radzen;
using CoreLibrary.Database;
using CoreLibrary.Repository;
using CoreLibrary.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddRadzenComponents();

builder.Services.AddTransient<BaseService>();
builder.Services.AddTransient<BaseRepository>();
builder.Services.AddTransient<ItemRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<ShopRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
