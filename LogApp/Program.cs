using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using LogApp.Service;
using Radzen;
using CoreLibrary.Repository;
using CoreLibrary.Service;
using CoreDB.DBLogApp;
using CoreDB.DBWebApp;
using Amazon;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;   
using Microsoft.AspNetCore.Server.Kestrel.Core;
using LogApp.Service;

// 웹루트 파일 지정
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "wwwroot"
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();

builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 100 * 1024 * 1024; // 100MB
});


builder.Services.AddRadzenComponents();

builder.Services.AddScoped<DbWebAppContext>();
builder.Services.AddScoped<DbLogAppContext>();


// AWS S3 클라이언트 등록
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddScoped<BaseService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<BaseRepository>();
builder.Services.AddScoped<ItemRepository>();
builder.Services.AddScoped<AccountRepository>();


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
app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
