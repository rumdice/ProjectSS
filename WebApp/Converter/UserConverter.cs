using CoreDB.DBWebApp;
using WepApp.DtoModels;

public static class UserConverter
{
    public static UserDto EntityToDto(this UserEntity userEntity)
    {
        return new UserDto
        {
            Id = userEntity.UserUid,
            Name = userEntity.Name,
            Level = userEntity.Level
        };
    }

    public static UserEntity DtoToEntity(this UserDto userDto)
    {
        return new UserEntity
        {
            UserUid = userDto.Id,
            Level = userDto.Level,
            Name = userDto.Name
        };
    }
}