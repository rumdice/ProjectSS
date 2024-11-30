using CoreDB.DBWebApp;
using Microsoft.AspNetCore.Http;
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
        IHttpContextAccessor httpContextAccessor,
        ILogger<ItemRepository> logger)
        : base (serviceProvider, httpContextAccessor, logger)
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