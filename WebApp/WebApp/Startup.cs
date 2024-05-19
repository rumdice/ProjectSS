using Microsoft.EntityFrameworkCore;
using WebApp.Reposotory;


namespace WebApp;

public class Startup()
{

    /// <summary>
    /// 서비스 설정
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices( IServiceCollection services )
    {
        services.AddControllers();

        // DB Context TODO: 이곳에 오는 것이 맞는가??
        // InMemmoryDB 를 사용할지 mysql을 사용할지?
        // services.AddDbContext<ItemContext>(opt =>
        //     opt.UseMySql());

        services.AddDbContext<ItemContext>(opt =>
            opt.UseInMemoryDatabase("ToDoList"))

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