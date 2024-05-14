using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using CoreLibrary;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // 문자열 테스트 함수 라이브러리 사용
        string input = "Test";
        var res = CoreLibrary.StringLibrary.PrintString(input); // 이렇게 해도 되고.
        Debug.WriteLine(res);

        string emptyStirng = "";
        var emptyRes = emptyStirng.PrintString(); // 확장 메서드로 사용. 
        Debug.WriteLine(emptyRes);

        _logger.LogDebug("디버깅 로그");
        _logger.LogInformation("인포 로깅");
        _logger.LogCritical("크리티컬한 로그 - 시스템 에러");
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}
