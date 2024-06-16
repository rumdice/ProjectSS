// using Microsoft.EntityFrameworkCore;
// using CoreLibrary;
// using Microsoft.Extensions.Configuration;
// using CoreLibrary.Database;


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

//         // Add DB Context
//         services.AddDbContext<DbWebAppContext>();
  
//         // Add Repository
//         services.AddTransient<ItemRepository>();
//         services.AddTransient<UserRepository>();

//         // Add Service
//         services.AddTransient<ItemService>();
//         services.AddTransient<UserService>();
        
//         // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//         services.AddEndpointsApiExplorer();
//         services.AddSwaggerGen();
//     } 


//     /// <summary>
//     /// 설정
//     /// </summary>
//     /// <param name="app"></param>
//     /// <param name="env"></param>
//     public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//     {
//         if(env.IsDevelopment())
//         {
//             app.UseSwagger();
//             app.UseSwaggerUI();
//         }

//         app.UseHttpsRedirection();
//         app.UseAuthentication();
//         app.UseRouting();

//         app.UseEndpoints(endpoints =>
//         {
//             endpoints.MapControllers();
//         });  
//     }

// }