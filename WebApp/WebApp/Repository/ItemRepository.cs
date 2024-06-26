using CoreLibrary.Database;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repository;


public class ItemRepository
{
    private readonly DbWebAppContext _webContext;

    public ItemRepository(
        DbWebAppContext context
    )
    {
        _webContext = context;
    }

    // 모든 아이템 간단 정보를 리스트로 
    public Task<List<ItemSimpleEntity>> GetItemSimpleInfoByNameList(string name)
    {
        return _webContext.ItemSimpleEntities
            .AsNoTracking()
            .Where(n => n.Name == name)
            .ToListAsync();
    }

    public Task<List<ItemSimpleEntity>> GetItemSimpleInfoListByUserId(long userId)
    {
        return _webContext.ItemSimpleEntities
            .AsNoTracking()
            .Where(n => n.UserUid == userId)
            .ToListAsync();
    }

    public Task<ItemSimpleEntity?> GetItemSimpleEntityByName(string name)
    {
        return _webContext.ItemSimpleEntities
            .AsNoTracking()
            .Where(n => n.Name == name)
            .SingleOrDefaultAsync();
    }

    // 특정 아이템 간단 정보 단일
    public Task<ItemSimpleEntity?> GetSimpleItemInfoByItemTId(long itemTid)
    {
        return _webContext.ItemSimpleEntities
            .AsNoTracking()
            .Where(i => i.ItemTid == itemTid)
            .SingleOrDefaultAsync();
    }

    public Task<List<ItemSimpleEntity>> GetSimpleItemAllList()
    {
        return _webContext.ItemSimpleEntities
            .AsNoTracking()
            .ToListAsync();
    }

    public Task UpdateRangeAsync( IEnumerable<ItemSimpleEntity> entities )
    {
        _webContext.ItemSimpleEntities.UpdateRange( entities );
        return _webContext.SaveChangesAsync();
    }

    public Task RemoveRangeAsync( IEnumerable<ItemSimpleEntity> entities )
    {
        _webContext.ItemSimpleEntities.RemoveRange( entities );
        return _webContext.SaveChangesAsync();
    }

    public Task InsertRangeAsync( IEnumerable<ItemSimpleEntity> entities )
    {
        _webContext.ItemSimpleEntities.AddRange( entities );
        return _webContext.SaveChangesAsync();
    }

    public Task DeleteAsync(ItemSimpleEntity entity)
    {
        _webContext.ItemSimpleEntities.Remove(entity);
        return _webContext.SaveChangesAsync();
    }
    
}