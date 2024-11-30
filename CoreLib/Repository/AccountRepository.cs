using CoreDB.DBLogApp;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Repository;

public class AccoutRepository : BaseRepository
{
    public AccoutRepository(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AccoutRepository> logger)
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

    public Task<AccountEntity?> GetByUserId(string aid)
    {
        return _logDbContext.AccountEntities
            .AsNoTracking()
            .Where(e => e.Aid == aid)
            .SingleOrDefaultAsync();
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