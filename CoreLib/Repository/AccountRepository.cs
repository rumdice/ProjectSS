using CoreDB.DBLogApp;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Repository;

public class AccountRepository : BaseRepository
{
    public AccountRepository(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AccountRepository> logger)
        : base (serviceProvider, httpContextAccessor, logger)
    {
    }

    /// <summary>
    /// 유저에 대한 단일 정보를 유저 이름 기반으로 가져오기
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<AccountEntity?> GetByName(string name)
    {
        return _logDbContext.AccountEntities
            .AsNoTracking()
            .Where(e => e.Name == name)
            .SingleOrDefaultAsync();
    }

    public async Task<AccountEntity?> GetById(string aid)
    {
        return await _logDbContext.AccountEntities
            .AsNoTracking()
            .Where(e => e.Aid == aid)
            .FirstOrDefaultAsync();
    }
    
    public async Task UpdateAsync(AccountEntity entity)
    {
        _logDbContext.Update(entity);
        await _logDbContext.SaveChangesAsync();
    }

    public async Task InsertAsync(AccountEntity entity)
    {
        _logDbContext.Add(entity);
        await _logDbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(AccountEntity entity)
    {
        _logDbContext.Remove(entity);
        await _logDbContext.SaveChangesAsync();
    }

}