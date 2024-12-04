using CoreDB.DBWebApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreLibrary.Repository;

// TODO: 구조에 대한 고민.
// CoreLib에는 인터페이스 성격의 Base~ 남기고 하위 구현은 ~App 으로 넘기고 싶다.
// 그런데 WebApp에 구현된 ItemRepository 를 LogApp이 쓰고 싶은 경우는 어떻게 해야 하는가?
// 1. 명확하게 ~App 에서 쓰이는 것만 하위 구현 하고 공용 구현은 Base에 남긴다.
// - 기준이 애매해지고 변경시 바꾸기 번거로워 진다.
// 2. 그냥 전부 CoreLib에서 구현한다.
// - 지금이랑 똑같다.

public class ItemRepository : BaseRepository
{
    public ItemRepository(
        IServiceProvider serviceProvider,
        ILogger<ItemRepository> logger)
        : base (serviceProvider, logger)
    {
    }

    // 모든 아이템 간단 정보를 리스트로 
    public Task<List<ItemEntity>> GetItemSimpleInfoByNameList(string name)
    {
        return _webDbContext.ItemEntity
            .AsNoTracking()
            .Where(n => n.name == name)
            .ToListAsync();
    }

    public Task<List<ItemEntity>> GetItemSimpleInfoListByUserId(long userId)
    {
        return _webDbContext.ItemEntity
            .AsNoTracking()
            .Where(n => n.uid == userId)
            .ToListAsync();
    }

    public Task<ItemEntity?> GetItemSimpleEntityByName(string name)
    {
        return _webDbContext.ItemEntity
            .AsNoTracking()
            .Where(n => n.name == name)
            .SingleOrDefaultAsync();
    }

    // 특정 아이템 간단 정보 단일
    public Task<ItemEntity?> GetSimpleItemInfoByItemTId(long itemTid)
    {
        return _webDbContext.ItemEntity
            .AsNoTracking()
            .Where(i => i.tid == itemTid)
            .SingleOrDefaultAsync();
    }

    public Task<List<ItemEntity>> GetSimpleItemAllList()
    {
        return _webDbContext.ItemEntity
            .AsNoTracking()
            .ToListAsync();
    }

    public Task UpdateRangeAsync( IEnumerable<ItemEntity> entities )
    {
        _webDbContext.ItemEntity.UpdateRange( entities );
        return _webDbContext.SaveChangesAsync();
    }

    public Task RemoveRangeAsync( IEnumerable<ItemEntity> entities )
    {
        _webDbContext.ItemEntity.RemoveRange( entities );
        return _webDbContext.SaveChangesAsync();
    }

    public Task InsertRangeAsync( IEnumerable<ItemEntity> entities )
    {
        _webDbContext.ItemEntity.AddRange( entities );
        return _webDbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(ItemEntity entity)
    {
        _webDbContext.ItemEntity.Remove(entity);
        return _webDbContext.SaveChangesAsync();
    }
    
}