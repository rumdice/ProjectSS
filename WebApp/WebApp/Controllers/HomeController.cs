
﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CoreLibrary;
using WebApp.Models;
using WepApp.DtoModels;

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
        
        
        // API 경로가 다르지만 기반 기능 추가 후 분산 시키기 
        _logger.LogInformation("아이템을 가져온다");
        
        // return View(); // 에러가 발생 함 return viewModel을 해야 함.

        // TODO : Entity Frame work 연동 및 repository 관련 작업 후 db에서 정보를 가져오기
        // 가져왔다고 치고 일단 아이템 데이터의 하드코딩. DDD 설계 관점에서 이건 entity가 된다.
        var itemInfo = new ItemSimpleInfoDto
        {
            Id = 1001,
            Name = "세계를 가르는 슈퍼 필살 어쩌구 검",
            Grade = 1, // Enum으로 처리하면 좋을 듯 1:회색 똥템 ~ 5:레어 6:유물 7:전설 등.
        };

        return new GetItemSimpleInfoViewModelResponse(
                itemInfo
            ).GetActionResult(this);
    }
}
