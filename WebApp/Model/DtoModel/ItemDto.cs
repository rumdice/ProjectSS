namespace WepApp.DtoModels;


/// <summary>
/// 아이템의 간단한 정보 : 서비스 레벨에서 표현 레벨(컨트롤러) 로 데이터 전송시 쓰이는 데이터 모델링
/// (TODO: 따라서 유저 고유값 UserUid는 경우에 따라 필요없을 수 있다.)
/// </summary>
public class ItemSimpleInfoDto
{
    public long? ItemTid { get; set; } // 아이템 고유 기획 테이블 ID
    
    public string? Name { get; set; } // 아이템 이름
    
    public int? Grade { get; set; } // 아이템 등급
}

// Dto 와 Entity 간의 관계에서 nullable이 중요함.
