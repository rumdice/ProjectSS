using WebApp.ViewModels;
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


public class SetUserNameViewModelResponse : CodeResponseViewModel<ServiceResponseCode>
{
    public SetUserNameViewModelResponse(
        ServiceResponseCode code
    ) : base(code)
    {
    }
}


public class DeleteUserViewModelResponse : CodeResponseViewModel<ServiceResponseCode>
{
    public DeleteUserViewModelResponse(
        ServiceResponseCode code
    ) : base(code)
    {
    }
}


