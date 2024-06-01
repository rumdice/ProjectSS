using Microsoft.EntityFrameworkCore;
using WebApp.Database;


public class UserRepository
{
    private readonly UserContext _userContext;

    public UserRepository(
        UserContext context
    )
    {
        _userContext = context;
    }

    /// <summary>
    /// 유저에 대한 정보
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<UserEntity?> GetUserInfoByName(string name)
    {
        return _userContext.UserEntity
            .AsNoTracking()
            .Where(e => e.Name == name)
            .SingleOrDefaultAsync();
    }


}