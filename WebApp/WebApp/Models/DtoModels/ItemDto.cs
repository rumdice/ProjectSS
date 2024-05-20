namespace WepApp.DtoModels;

// DB의 모양을 객체로 모델링 하기 위함 (DTO Model)
// 상속 관계를 만들어야 하는가?
// 클라이언트에게 response 해주는 viewModel
// nullable을 허용 해야 하는가?
// 서버의 Dto Layer - 클라이언트 간의 관계


/// <summary>
/// 아이템의 간단한 정보라 치자.
/// </summary>
public class ItemSimpleInfoDto
{
    public long Id { get; set; } // 아이템 고유 기획 테이블 ID
    
    public string? Name { get; set; } // 아이템 이름 : 
    
    public int Grade { get; set; } // 아이템 등급
}
