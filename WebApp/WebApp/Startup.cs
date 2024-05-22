using Microsoft.EntityFrameworkCore;
using WebApp.Database;
using CoreLibrary;
using Microsoft.Extensions.Configuration;


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

        // TODO: 코드 중복 줄이기. 컨텍스트가 계속 늘어남. 추상화 레벨 올리기
        services.AddDbContext<ItemContext>(options =>
            options.UseMySql(
                Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(11, 3, 2))
            ));
        
        // Add Repositorys
        services.AddTransient<ItemRepository>();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    } 


    /// <summary>
    /// 설정
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if(env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });  
    }

}