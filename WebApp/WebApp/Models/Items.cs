namespace WepApp.Models;


// DB의 모양을 객체로 모델링 하기 위함

public class Item
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}