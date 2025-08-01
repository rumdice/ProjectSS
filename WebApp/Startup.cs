using Microsoft.EntityFrameworkCore;
using CoreLibrary;
using CoreDB.DBLogApp;
using CoreDB.DBWebApp;
using CoreLibrary.Repository;
using CoreLibrary.Service;


using WebApp.Service;
using WebApp.MessageHub;


namespace WebApp;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// 서비스 설정
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices( IServiceCollection services )
    {
        services.AddControllers();
        services.AddHttpContextAccessor();

        // Core.DB
        // db 연결 셋팅
        var connectionStringWeb = "Server=localhost;Port=3306;Database=db_WebApp;User=root;Password=pass1234";
        services.AddDbContext<DbWebAppContext>(options =>
            options.UseMySql(connectionStringWeb, new MariaDbServerVersion(new Version(11, 6, 2))));

        var connectionStringLog = "Server=localhost;Port=3306;Database=db_LogApp;User=root;Password=pass1234";
        services.AddDbContext<DbLogAppContext>(options =>
            options.UseMySql(connectionStringLog, new MariaDbServerVersion(new Version(11, 6, 2))));

        // Add DB Context
        services.AddScoped<DbWebAppContext>();
        services.AddScoped<DbLogAppContext>();
  


        // Core.Lib
        // Add Service
        services.AddSingleton<BaseService>();
        
        
        // Add Repository
        services.AddTransient<BaseRepository>();
        services.AddTransient<ItemRepository>();
        services.AddTransient<UserRepository>();
        services.AddTransient<ShopRepository>();

        services.AddSingleton(typeof(BaseLogger<>)); // 커스텀 구현체로 등록

        // App Service
        services.AddScoped<ItemService>();
        services.AddTransient<UserService>();
        services.AddTransient<ShopService>();

        // background service
        services.AddHostedService<MessageBackgroundService>();
        services.AddSingleton<MessageConsumeService>();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        // Add SignalR
        services.AddSignalR();
    } 


    /// <summary>
    /// 설정
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // 개발환경에서만 스웨거 사용
        //if(env.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }

        // 모든 환경에서 스웨거 사용
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp API V1");
        });

        
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            // Map SignalR Hubs
            endpoints.MapHub<ChatHub>("/MessageHub"); // 체팅 메시지헙
        });  

        
    }

}