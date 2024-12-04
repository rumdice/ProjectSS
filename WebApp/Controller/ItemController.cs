using CoreDB.DBWebApp;
using Microsoft.AspNetCore.Mvc;
using WebApp.Converter;
using WebApp.Models;
using WebApp.Service;
using WebApp.ViewModels;
using WepApp.DtoModels;


namespace WebApp.Controller;


[Route("[Controller]/[action]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly ItemService _itemService;
    private readonly ILogger<ItemController> _logger;

    public ItemController(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ItemController> logger)
    {
        _itemService = serviceProvider.GetRequiredService<ItemService>();
        _logger = logger;
    }

    [HttpGet]
    public async Task<ItemEntity?> Get(long itemTid)
    {
        var itemSimpleEntity = await _itemService.GetSimpleItemResultAsync(itemTid);
        if (itemSimpleEntity == null)
        {
            throw new Exception("itemSimpleEntity Result is null");
        }
        return itemSimpleEntity;
    }

    [HttpPost]
    public async Task<IActionResult> GetItemSimpleInfo(GetItemSimpleInfoViewModelRequest request)
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

            // 가져왔다고 치고 일단 아이템 데이터의 하드코딩. DDD 설계 관점에서 이건 entity가 된다.
            // 컨트롤러 - 서비스 레벨은 Dto를 사용해야 한다 (Entity는 사용하면 안된다.)
            var itemSimpleInfoDto = new ItemSimpleInfoDto
            {
                ItemTid = 1001,
                Name = "세계를 가르는 슈퍼 필살 어쩌구 검",
                Grade = 1, // Enum으로 처리하면 좋을 듯 1:회색 똥템 ~ 5:레어 6:유물 7:전설 등.
            };

            long itemTid = 1002; // tid가 잘못 입력 될 가능성이 있다.
            var itemSimpleEntity = await _itemService.GetSimpleItemResultAsync(itemTid);
            if (itemSimpleEntity == null)
            {
                throw new Exception("itemSimpleEntity Result is null");
            }

            // 컨버팅 작업
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