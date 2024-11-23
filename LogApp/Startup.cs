// using Microsoft.EntityFrameworkCore;
// using CoreLibrary;
// using Microsoft.Extensions.Configuration;
// using CoreLibrary.Database;
// using CoreLibrary.Repository;
// using CoreLibrary.Service;
// using Microsoft.AspNetCore.Components;
// using Microsoft.AspNetCore.Components.Web;
// using LogApp.Data;
// using Radzen;

// namespace LogApp;

// public class Startup
// {
//     public IConfiguration Configuration { get; }

//     public Startup(IConfiguration configuration)
//     {
//         Configuration = configuration;
//     }

//     /// <summary>
//     /// 서비스 설정
//     /// </summary>
//     /// <param name="services"></param>
//     public void ConfigureServices( IServiceCollection services )
//     {
//         services.AddControllers();
//         services.AddHttpContextAccessor();

//         services.AddRazorPages();
//         services.AddServerSideBlazor();
//         services.AddSingleton<WeatherForecastService>();
//         services.AddRadzenComponents();


//         // BASE
//         // Add Repository
//         services.AddTransient<BaseRepository>();
//         services.AddTransient<ItemRepository>();
//         services.AddTransient<UserRepository>();
//         services.AddTransient<ShopRepository>();

//         // Add Service
//         services.AddTransient<BaseService>();
        
//         // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//         //services.AddEndpointsApiExplorer();
//         //services.AddSwaggerGen();
//         // Add SignalR
//         services.AddSignalR();
//     } 


//     /// <summary>
//     /// 설정
//     /// </summary>
//     /// <param name="app"></param>
//     /// <param name="env"></param>
//     public void Configure(IWebHostEnvironment env)
//     {
//         // 개발환경에서만 스웨거 사용
//         //if(env.IsDevelopment())
//         // {
//         //     app.UseSwagger();
//         //     app.UseSwaggerUI();
//         // }

//         // Configure the HTTP request pipeline.
//         if (!app.Environment.IsDevelopment())
//         {
//             app.UseExceptionHandler("/Error");
//             // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//             app.UseHsts();
//         }

//         app.UseHttpsRedirection();
//         app.UseAuthentication();
//         app.UseRouting();
//         app.UseStaticFiles();
//         app.MapBlazorHub();
//         app.MapFallbackToPage("/_Host");

//     }

// }