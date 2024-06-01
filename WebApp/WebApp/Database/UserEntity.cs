// TODO: 이부분에 대한 생성 로직을 코드 분리
// 1. code first 
// 2. Database frist


using System.ComponentModel.DataAnnotations;

public class UserEntity
{
    [Key]
    public long UserUid { get; set; }

    public string? Name { get; set; }

    public int Level { get; set; }
}