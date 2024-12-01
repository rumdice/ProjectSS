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

builder.Services.AddHttpContextAccessor(); // 클라이언트 측(razor)에서 HTTPContext에 접근하기 위해 사용

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();

builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
});

// 업로드 파일 허용 크기 
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 100 * 1024 * 1024; // 100MB
});

// 로그인 및 세션의 개념을 사용하기 위함.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 유지시간 30분
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseSession(); // 세션을 사용한다.

app.UseRouting();
app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
