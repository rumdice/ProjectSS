using CoreLibrary.Database;
using Microsoft.EntityFrameworkCore;


public class UserRepository
{
    private readonly DbWebAppContext _webContext;

    public UserRepository(
        DbWebAppContext context
    )
    {
        _webContext = context;
    }

    /// <summary>
    /// 유저에 대한 단일 정보를 유저 이름 기반으로 가져오기
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<UserEntity?> GetUserInfoByName(string name)
    {
        return _webContext.UserEntities
            .AsNoTracking()
            .Where(e => e.Name == name)
            .SingleOrDefaultAsync();
    }


}