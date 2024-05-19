
namespace WebApp.Models;

// 클라이언트에게 response 해주는 viewModel
// nullable을 허용 해야 하는가?
// 서버의 Dto Layer - 클라이언트 간의 관계

public class ItemView
{
    public long Id { get; set; }
    public string? Name { get; set; } = "";

}