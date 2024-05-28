

using Microsoft.EntityFrameworkCore;
using WebApp.Database;
using WepApp.DtoModels;

public class ItemRepository
{
    private readonly ItemContext _itemContext;

    public ItemRepository(
        ItemContext context
    )
    {
        _itemContext = context;
    }

    // 모든 아이템 간단 정보 목록
    public Task<List<ItemSimpleEntity>> GetItemSimpleInfoListByName(string name)
    {
        return _itemContext.ItemSimpleEntity
            .AsNoTracking()
            .Where(n => n.Name == name)
            .ToListAsync();
    }

    // 특정 아이템 간단 정보 단일
    public Task<ItemSimpleEntity?> GetSimpleItemInfoByItemTId(long itemTid)
    {
        return _itemContext.ItemSimpleEntity
            .AsNoTracking()
            .Where(i => i.ItemTid == itemTid)
            .SingleOrDefaultAsync();
    }
    
}