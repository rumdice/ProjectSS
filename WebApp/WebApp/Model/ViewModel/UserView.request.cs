using System.ComponentModel.DataAnnotations;


/// <summary>
/// 유저의 정보를 요청하는 패킷
/// </summary>
public class GetUserInfoViewModelRequest
{
    [Required]
    public long UserUid { get; set; } // 유저의 고유 ID
}


public class SetUserNameViewModelRequest
{
    [Required]
    public long UserUid { get; set; } // TODO: 차후 세션 관리로 뺀다.

    [Required]
    public string UserName { get; set; } = string.Empty;
}

public class AddNewUserViewModelRequest
{
    [Required]
    public long UserUid { get; set; }

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public int UserLevel { get; set; }
}