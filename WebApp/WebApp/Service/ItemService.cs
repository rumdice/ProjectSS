using CoreLibrary.Database;
using Microsoft.AspNetCore.JsonPatch.Internal;

// Service : 비즈니스 로직을 처리하는 단계. 간단히 자료를 가져오는 것 부터 복잡한 쿼리 연계까지

public class ItemService
{
    private readonly ItemRepository _itemRepository;
    private readonly ILogger<ItemService> _logger;

    public ItemService( 
        ItemRepository itemRepository,
        ILogger<ItemService> logger
    )
    {
        _itemRepository = itemRepository;
        _logger = logger;
    }

    public async Task<ItemSimpleEntity?> GetSimpleItemResultAsync(long itemTid)
    {
        return await _itemRepository.GetSimpleItemInfoByItemTId(itemTid);
    }

    public async Task<List<ItemSimpleEntity>> GetItemSimpleInfoListByNameAsync(string name)
    {
        return await _itemRepository.GetItemSimpleInfoByNameList(name);
    }

    public async Task<List<ItemSimpleEntity>> GetItemSimpleInfoListByUserIdAsync(long userUid)
    {
        return await _itemRepository.GetItemSimpleInfoListByUserId(userUid);
    }

    public async Task<ItemSimpleEntity?> GetItemSimpleResultByName(string name)
    {
        return await _itemRepository.GetItemSimpleEntityByName(name);
    }
}
