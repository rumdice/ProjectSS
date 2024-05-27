

using Microsoft.EntityFrameworkCore;
using WebApp.Database;
using WepApp.DtoModels;

public class ItemRepository
{
    private readonly ItemContext _itemContext;
    private readonly ILogger<ItemRepository> _logger; // TODO: 로거가 이 레벨에서 필요할까? 에러 발생시 로깅 작업 목적

    public ItemRepository(
        ItemContext context,
        ILogger<ItemRepository> logger
    )
    {
        _itemContext = context;
        _logger = logger;
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