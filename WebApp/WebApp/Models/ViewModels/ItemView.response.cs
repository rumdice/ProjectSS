
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;
using WepApp.DtoModels;

namespace WebApp.Models;


/// <summary>
/// 아이템 심플 정보 응답 viewModel
/// </summary>
public class GetItemSimpleInfoViewModelResponse : CodeResponseViewModel<ServiceResponseCode>
{
    public ItemSimpleInfoDto? ItemSimpleInfo { get; } // 아이템의 간단한 정보

    public GetItemSimpleInfoViewModelResponse( 
        ServiceResponseCode code,
        ItemSimpleInfoDto? itemSimpleInfo = null) : base(code)
    {
        this.ItemSimpleInfo = itemSimpleInfo;
    }

    // TODO: 사용자 contoller 레벨에서 ExceptionResponseViewModel를 그냥 쓰면 되지 않을까?
    // 컨텐츠 특화의 예외에 대한 viewModel이 필요할 수는 있다.
    
    // public GetItemSimpleInfoViewModelResponse(
    //     ServiceResponseCode code,
    //     Exception e ) : base( code, e )
    // {
    // }
}


