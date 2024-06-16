using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BackOfficeApp;
using Microsoft.AspNetCore;

namespace BackOfficeApp.Main;

public class Program
{
    public static async Task Main(string[] args)
    {
        await ConfigureWebHostDefaults(args).RunAsync();
    }

    private static WebAssemblyHost ConfigureWebHostDefaults( string[] args )
    {
        // var port = 7001;
        // webBuilder.UseStartup<Startup>();
        // webBuilder.UseUrls( $"http://0.0.0.0:{port}" );

        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        var startUp = new Startup(builder);
        return startUp.BuildApp();
    }

}

