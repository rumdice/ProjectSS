namespace WepApp.Models;


// DB의 모양을 객체로 모델링 하기 위함 (DTO Model)

// 상속 관계를 만들어야 하는가?

public class ItemDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}


public class UserDto
{
    public long Id { get; set; }

}