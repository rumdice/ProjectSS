
﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoreLibrary;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost("[action]")]
    public IActionResult Index()
    {
        // 문자열 테스트 함수 라이브러리 사용
        string input = "Test";
        var res = StringLibrary.PrintString(input); // 이렇게 해도 되고.
        Debug.WriteLine(res);

        string emptyStirng = "";
        var emptyRes = emptyStirng.PrintString(); // 확장 메서드로 사용. 
        Debug.WriteLine(emptyRes);
        Debug.WriteLine(emptyRes);

        _logger.LogDebug("디버깅 로그");
        _logger.LogInformation("인포 로깅");
        _logger.LogCritical("크리티컬한 로그 - 시스템 에러");
        _logger.LogInformation("인포메이션 로그");
        
        return View(); // 에러가 발생 함 return viewModel을 해야 함.
    }
}
