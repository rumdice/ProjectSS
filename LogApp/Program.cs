using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using LogApp.Data;
using LogApp.Service;
using Radzen;
using CoreLibrary.Database;
using CoreLibrary.Repository;
using CoreLibrary.Service;

var builder = WebApplication.CreateBuilder(args);

// Add IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();


builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddRadzenComponents();

builder.Services.AddScoped<DbWebAppContext>();
  
builder.Services.AddScoped<BaseService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<BaseRepository>();
builder.Services.AddScoped<ItemRepository>();

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
