
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;


/// <summary>
/// 아이템의 간략한 정보를 요청하는 패킷
/// </summary>
public class GetItemSimpleInfoViewModelRequest
{
    [Required]
    public ulong UserUid { get; set; } // 아이템의 소유자 user 고유 ID, 차후 세션 값으로 빼기

    [Required]
    public ulong ItemTid { get; set; } // 아이템의 기획 테이블 고유 id

    public ulong ItemUid { get; set; } = 0; // 유저가 소유한 아이템의 고유ID
}