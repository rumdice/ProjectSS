
using Microsoft.AspNetCore.Mvc;
using WepApp.DtoModels;

namespace WebApp.Models;

// 클라이언트에게 response 해주는 viewModel
// nullable을 허용 해야 하는가?
// 서버의 Dto Layer - 클라이언트 간의 관계

public class ItemView
{
    public long Id { get; set; }
    public string? Name { get; set; } = "";

}

public class GetItemSimpleInfoViewModelResponse
{
    public ItemSimpleInfoDto? ItemSimpleInfo { get; } // 아이템의 간단한 정보

    public GetItemSimpleInfoViewModelResponse(ItemSimpleInfoDto? itemSimpleInfo = null)
    {
        this.ItemSimpleInfo = itemSimpleInfo;
    }

    public GetItemSimpleInfoViewModelResponse(Exception exception)
    {
        // TODO : 예외처리
    }

    // 코드 중복?
    public IActionResult GetActionResult(ControllerBase controller)
    {
        return controller.Ok(this);
    }

}


