using System.ComponentModel.DataAnnotations;

namespace WebApp.Database;

/// <summary>
/// 아이템의 간단한 정보 : 테이블 매핑을 모델화 한 Entity
/// </summary>
public class ItemSimpleEntity
{
    [Key]
    public long UserUid { get; set; } // 소유한 유저의 고유 userId

    public long ItemTid { get; set; } // 아이템 고유 기획 테이블 ID
    
    public string? Name { get; set; } // 아이템 이름
    
    public int Grade { get; set; } // 아이템 등급
}
