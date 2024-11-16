using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BackOfficeApp;
using Microsoft.AspNetCore;

public class Startup
{
    public IConfiguration Configuration { get; }

    public readonly WebAssemblyHostBuilder Builder;

    // public Startup(IConfiguration configuration)
    // {
    //     Configuration = configuration;
    // }

    public Startup(WebAssemblyHostBuilder builder)
    {
        this.Builder = builder;

        this.Builder.RootComponents.Add<App>("#app");
        this.Builder.RootComponents.Add<HeadOutlet>("head::after");

        this.Builder.Services.AddScoped(sp => 
            new HttpClient 
            { 
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
            }
        );
    }

    public WebAssemblyHost BuildApp()
    {
        return this.Builder.Build();
    }

    /// <summary>
    /// 서비스 설정
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices( IServiceCollection services )
    {
        
    } 


    /// <summary>
    /// 설정
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure()
    {
          
    }

}