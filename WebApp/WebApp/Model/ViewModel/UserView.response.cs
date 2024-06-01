using CoreLibrary.ViewModels;
using WepApp.DtoModels;


namespace WebApp.Models;

public class GetUserInfoViewModelResponse : CodeResponseViewModel<ServiceResponseCode>
{
    public UserDto? userInfo { get; } //  유저에 대한 정보 (사용자 계정)

    public GetUserInfoViewModelResponse(
        ServiceResponseCode code,
        UserDto? userInfo = null) : base(code)
    {
        this.userInfo = userInfo;
    }

}


