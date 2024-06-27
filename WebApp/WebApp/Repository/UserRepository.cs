using CoreLibrary.Database;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repository;

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

    public Task<UserEntity?> GetUserInfoByUserId(long userId)
    {
        return _webContext.UserEntities
            .AsNoTracking()
            .Where(e => e.UserUid == userId)
            .SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(UserEntity entity)
    {
        _webContext.Update(entity);
        await _webContext.SaveChangesAsync();
    }

    public async Task InsertAsync(UserEntity entity)
    {
        _webContext.Add(entity);
        await _webContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(UserEntity entity)
    {
        _webContext.Remove(entity);
        await _webContext.SaveChangesAsync();
    }

}