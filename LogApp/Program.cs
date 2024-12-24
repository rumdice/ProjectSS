using LogApp.Service;
using Radzen;
using CoreLibrary.Repository;
using CoreLibrary.Service;
using Amazon.S3;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using CoreDB.DBLogApp;
using CoreDB.DBWebApp;
using Microsoft.EntityFrameworkCore;
using CoreLibrary;

// 웹루트 파일 지정
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = "wwwroot"
});

builder.Services.AddHttpContextAccessor(); // 클라이언트 측(razor)에서 HTTPContext에 접근하기 위해 사용
builder.Services.AddHttpClient();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
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
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30); // 유지시간 30분
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

builder.Services.AddRadzenComponents();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
string dbAddress = "localhost";
if (environment != "Development")
{
    dbAddress = "host.docker.internal";
}

// Core.DB
// DB 연결 셋팅
var connectionStringWeb = $"Server={dbAddress};Port=3306;Database=db_WebApp;User=root;Password=pass1234";
builder.Services.AddDbContext<DbWebAppContext>(options =>
    options.UseMySql(connectionStringWeb, new MariaDbServerVersion(new Version(11, 6, 2))));

var connectionStringLog = $"Server={dbAddress};Port=3306;Database=db_LogApp;User=root;Password=pass1234";
builder.Services.AddDbContext<DbLogAppContext>(options =>
    options.UseMySql(connectionStringLog, new MariaDbServerVersion(new Version(11, 6, 2))));

builder.Services.AddScoped<DbWebAppContext>();
builder.Services.AddScoped<DbLogAppContext>();

// 
builder.Services.AddScoped<DialogService>();

// Core.lib
builder.Services.AddScoped<BaseService>();
builder.Services.AddScoped<BaseRepository>();
builder.Services.AddScoped<ItemRepository>();
builder.Services.AddScoped<AccountRepository>();

//builder.Services.AddScoped<Logger<BaseService>>();
builder.Services.AddSingleton(typeof(BaseLogger<>)); // 커스텀 구현체로 등록


// AWS S3 클라이언트 등록
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

// App Service
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<MessageService>();



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
//app.UseSession(); // 세션을 사용한다.

app.UseRouting();
app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
