using CoreDB.DBWebApp;
using WepApp.DtoModels;

public static class UserConverter
{
    public static UserDto EntityToDto(this UserEntity userEntity)
    {
        return new UserDto
        {
            Id = userEntity.uid,
            Name = userEntity.name,
            Level = userEntity.level
        };
    }

    public static UserEntity DtoToEntity(this UserDto userDto)
    {
        return new UserEntity
        {
            uid = userDto.Id,
            level = userDto.Level,
            name = userDto.Name
        };
    }
}