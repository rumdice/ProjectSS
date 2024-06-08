using CoreLibrary.ViewModels;
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
}