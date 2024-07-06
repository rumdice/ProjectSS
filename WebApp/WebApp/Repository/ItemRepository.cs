using CoreLibrary.Database;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repository;


public class ItemRepository : BaseRepository
{
    public ItemRepository(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ItemRepository> logger,
        DbWebAppContext webAppContext)
        : base (serviceProvider, httpContextAccessor, logger, webAppContext)
    {
    }

    // 모든 아이템 간단 정보를 리스트로 
    public Task<List<ItemSimpleEntity>> GetItemSimpleInfoByNameList(string name)
    {
        return _webDbContext.ItemSimpleEntities
            .AsNoTracking()
            .Where(n => n.Name == name)
            .ToListAsync();
    }

    public Task<List<ItemSimpleEntity>> GetItemSimpleInfoListByUserId(long userId)
    {
        return _webDbContext.ItemSimpleEntities
            .AsNoTracking()
            .Where(n => n.UserUid == userId)
            .ToListAsync();
    }

    public Task<ItemSimpleEntity?> GetItemSimpleEntityByName(string name)
    {
        return _webDbContext.ItemSimpleEntities
            .AsNoTracking()
            .Where(n => n.Name == name)
            .SingleOrDefaultAsync();
    }

    // 특정 아이템 간단 정보 단일
    public Task<ItemSimpleEntity?> GetSimpleItemInfoByItemTId(long itemTid)
    {
        return _webDbContext.ItemSimpleEntities
            .AsNoTracking()
            .Where(i => i.ItemTid == itemTid)
            .SingleOrDefaultAsync();
    }

    public Task<List<ItemSimpleEntity>> GetSimpleItemAllList()
    {
        return _webDbContext.ItemSimpleEntities
            .AsNoTracking()
            .ToListAsync();
    }

    public Task UpdateRangeAsync( IEnumerable<ItemSimpleEntity> entities )
    {
        _webDbContext.ItemSimpleEntities.UpdateRange( entities );
        return _webDbContext.SaveChangesAsync();
    }

    public Task RemoveRangeAsync( IEnumerable<ItemSimpleEntity> entities )
    {
        _webDbContext.ItemSimpleEntities.RemoveRange( entities );
        return _webDbContext.SaveChangesAsync();
    }

    public Task InsertRangeAsync( IEnumerable<ItemSimpleEntity> entities )
    {
        _webDbContext.ItemSimpleEntities.AddRange( entities );
        return _webDbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(ItemSimpleEntity entity)
    {
        _webDbContext.ItemSimpleEntities.Remove(entity);
        return _webDbContext.SaveChangesAsync();
    }
    
}