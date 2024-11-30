// Entity와 DtoModel 간의 컨버팅 역활을 한다.
// Entity는 Domain 계층
// DtoModel은 어플리케이션과 표현계층 둘다.
// 일단 확장 메서드 방식으로 구현한다.

using CoreDB.DBWebApp;
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

    // 빠진 자료는 어떻게 처리 해야 하나? 
    // 역방햐야 컨버터는 저장시 사용.
    // 보통은 entity가 dto 보다 가지고 있는 자료 형이 더 많다.
    // 일단은 Dto가 가지고 있는 값을 entity로 덮어 쓴다.
    // Update때 사용 할 듯

    public static ItemSimpleEntity DtoToEntity(this ItemSimpleInfoDto itemDto)
    {
        return new ItemSimpleEntity
        {
            ItemTid = itemDto.ItemTid,
            Name = itemDto.Name,
            Grade = itemDto.Grade  
        };
    }
}