

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Converter;
using WebApp.Database;
using WebApp.Models;
using WebApp.ViewModels;
using WepApp.DtoModels;

namespace WebApp.Controllers;


[Route("[Controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly ILogger<ItemController> _logger;
    private readonly ItemService _itemService;

    public ItemController(
        ILogger<ItemController> logger,
        ItemService itemService        
    )
    {
        _logger = logger;
        _itemService = itemService;
    }

    [HttpPost( "[action]" )]
    public async Task<IActionResult> GetItemSimpleInfoAsync(GetItemSimpleInfoViewModelRequest request)
    {
        try
        {
            // TODO: 패킷 검증 단계. 코드 중복이 예상되니 일반적인 패킷 검증은 attribute로 하는게 좋겠다.
            // 일반적인 에러 처리
            if (request == null)
            {
                throw new Exception("request is null");
            }

            // 컨텐츠에 특화된 예외처리. 일단 예를 들어 Item Tid가 유효하지 않다고 친다.
            if (request.ItemTid < 0)
            {
                // 컨텐츠 특화 에러는 에러를 던지지 않고 viewModel return 처리
                return new GetItemSimpleInfoViewModelResponse(
                    ServiceResponseCode.InvatildItemIid)
                .GetActionResult(this);
            }

            // TODO : Entity Frame work 연동 및 repository 관련 작업 후 db에서 정보를 가져오기
            // 가져왔다고 치고 일단 아이템 데이터의 하드코딩. DDD 설계 관점에서 이건 entity가 된다.
            // 컨트롤러 - 서비스 레벨은 Dto를 사용해야 한다 (Entity는 사용하면 안된다.)
            var itemSimpleInfoDto = new ItemSimpleInfoDto
            {
                ItemTid = 1001,
                Name = "세계를 가르는 슈퍼 필살 어쩌구 검",
                Grade = 1, // Enum으로 처리하면 좋을 듯 1:회색 똥템 ~ 5:레어 6:유물 7:전설 등.
            };

            long itemTid = 1002;
            var itemSimpleEntity = await _itemService.GetSimpleItemResultAsync(itemTid);
            if (itemSimpleEntity == null)
            {
                throw new Exception("itemSimpleEntity Result is null");
            }
            
            // 이곳은 표현 레이어 이므로 Entity를 직접 다루면 안된다.
            // Entity를 직접 가져와서 편집등을 하면 안된다.

            itemSimpleInfoDto = itemSimpleEntity.EntityToDto();

            _logger.LogInformation("아이템을 가져온다");

            return new GetItemSimpleInfoViewModelResponse(
                ServiceResponseCode.Success,
                itemSimpleInfoDto
            ).GetActionResult(this);  
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
            
            // 일반적인 에러에 대한 viewModel 처리
            return ExceptionResponseViewModel.GetActionResult(this, e);
        }
        
    }
}