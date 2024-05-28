// Entity와 DtoModel 간의 컨버팅 역활을 한다.
// Entity는 Domain 계층
// DtoModel은 어플리케이션과 표현계층 둘다.
// 일단 확장 메서드 방식으로 구현한다.

using WebApp.Database;
using WepApp.DtoModels;

namespace WebApp.Converter;

public static class ItemConverter
{
    public static ItemSimpleInfoDto EntityToDto(this ItemSimpleEntity itemSimpleEntity) 
    {
        return new ItemSimpleInfoDto
        {
            ItemTid = itemSimpleEntity.ItemTid,
            Name = itemSimpleEntity.Name,
            Grade = itemSimpleEntity.Grade
        };
    }

    // 반대로 필요하지 않을까...?
    // TODO : 프로그램의 요구 사항에 따라 반드시 Entity와 DTO가 1:1 맵핑 되는 것은 아니다.
    // public static ItemSimpleEntity DtoToEntity(this ItemSimpleInfoDto itemSimpleDto)
    // {
    //     return new ItemSimpleEntity
    //     {
    //     };
    // }
}