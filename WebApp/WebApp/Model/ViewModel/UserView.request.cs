using System.ComponentModel.DataAnnotations;


/// <summary>
/// 유저의 정보를 요청하는 패킷
/// </summary>
public class GetUserInfoViewModelRequest
{
    [Required]
    public ulong UserUid { get; set; } // 유저의 고유 ID
}

// TODO: userEntity 에 대한 정의 및 작업
// Entity Framework 사용
// 1. Code First 
// 2. Database First

// repository 작업 필요.

// 데이터베이스 부분을 따로 빼는것이 좋겠다.
// 프로젝트 분리
// 해당 프로젝트에서 둘다 시도 해보기? 
