using Microsoft.EntityFrameworkCore;
using WebApp.Database;

public class ItemRepository
{
    private readonly ItemContext _itemContext;


    public ItemRepository(
        ItemContext context
    )
    {
        _itemContext = context;
    }

    // 모든 아이템 간단 정보를 리스트로 
    public Task<List<ItemSimpleEntity>> GetItemSimpleInfoByNameList(string name)
    {
        return _itemContext.ItemSimpleEntity
            .AsNoTracking()
            .Where(n => n.Name == name)
            .ToListAsync();
    }

    public Task<ItemSimpleEntity?> GetItemSimpleEntityByName(string name)
    {
        return _itemContext.ItemSimpleEntity
            .AsNoTracking()
            .Where(n => n.Name == name)
            .SingleOrDefaultAsync();
    }

    // 특정 아이템 간단 정보 단일
    public Task<ItemSimpleEntity?> GetSimpleItemInfoByItemTId(long itemTid)
    {
        return _itemContext.ItemSimpleEntity
            .AsNoTracking()
            .Where(i => i.ItemTid == itemTid)
            .SingleOrDefaultAsync();
    }

    // 모든 아이템에 대한 검색 기능 (필요한가?)
    // 이 쿼리의 경우 가져와서 가공해서 사용하는 식으로?
    public Task<List<ItemSimpleEntity>> GetSimpleItemAllList()
    {
        return _itemContext.ItemSimpleEntity
            .AsNoTracking()
            .ToListAsync();
    }
    
}