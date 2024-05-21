namespace WepApp.DtoModels;


/// <summary>
/// 아이템의 간단한 정보라 치자.
/// </summary>
public class ItemSimpleInfoDto
{
    public long Id { get; set; } // 아이템 고유 기획 테이블 ID
    
    public string? Name { get; set; } // 아이템 이름 : 
    
    public int Grade { get; set; } // 아이템 등급
}
