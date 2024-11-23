using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BackOfficeApp;
using Microsoft.AspNetCore;
using Radzen;

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

        // 루트 컴포넌트 등록
        this.Builder.RootComponents.Add<App>("#app");
        this.Builder.RootComponents.Add<HeadOutlet>("head::after");

        // HttpClient 서비스 등록
        this.Builder.Services.AddScoped(sp => 
            new HttpClient 
            { 
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
            }
        );

        // Radzen 구성 요소 등록
        this.Builder.Services.AddRadzenComponents();
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