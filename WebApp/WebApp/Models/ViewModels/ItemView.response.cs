
using Microsoft.AspNetCore.Mvc;
using WepApp.DtoModels;

namespace WebApp.Models;


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


