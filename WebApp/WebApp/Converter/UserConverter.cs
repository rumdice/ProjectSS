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
}