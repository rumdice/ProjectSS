using CoreDB.DBLogApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Repository;

public class AccountRepository : BaseRepository
{
    public AccountRepository(IServiceProvider serviceProvider) 
        : base(serviceProvider)
    {
    }

    /// <summary>
    /// 유저에 대한 단일 정보를 유저 이름 기반으로 가져오기
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<AccountEntity?> GetByName(string name)
    {
        return _logDbContext.AccountEntity
            .AsNoTracking()
            .Where(e => e.name == name)
            .SingleOrDefaultAsync();
    }

    public async Task<List<AccountEntity>> GetAll()
    {
        return await _logDbContext.AccountEntity
            .AsNoTracking()
            .ToListAsync();
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