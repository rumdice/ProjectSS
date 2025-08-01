using CoreDB.DBWebApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Repository;

public class UserRepository : BaseRepository
{
    public UserRepository(
        IServiceProvider serviceProvider) 
        : base (serviceProvider)
    {
    }

    /// <summary>
    /// 유저에 대한 단일 정보를 유저 이름 기반으로 가져오기
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<UserEntity?> GetUserInfoByName(string name)
    {
        return _webDbContext.UserEntity
            .AsNoTracking()
            .Where(e => e.name == name)
            .SingleOrDefaultAsync();
    }

    public Task<UserEntity?> GetUserInfoByUserId(long userId)
    {
        return _webDbContext.UserEntity
            .AsNoTracking()
            .Where(e => e.uid == userId)
            .SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(UserEntity entity)
    {
        _webDbContext.Update(entity);
        await _webDbContext.SaveChangesAsync();
    }

    public async Task InsertAsync(UserEntity entity)
    {
        _webDbContext.Add(entity);
        await _webDbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(UserEntity entity)
    {
        _webDbContext.Remove(entity);
        await _webDbContext.SaveChangesAsync();
    }

}