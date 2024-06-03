using CoreLibrary.Database;
using Microsoft.EntityFrameworkCore;

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

    // 모든 아이템에 대한 검색 기능 (필요한가?)
    // 이 쿼리의 경우 가져와서 가공해서 사용하는 식으로?
    public Task<List<ItemSimpleEntity>> GetSimpleItemAllList()
    {
        return _webContext.ItemSimpleEntities
            .AsNoTracking()
            .ToListAsync();
    }
    
}