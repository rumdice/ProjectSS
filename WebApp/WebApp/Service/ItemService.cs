using CoreLibrary.Database;

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
        // 경우에 따라선 단순 쿼리 실행이 아닌 복잡한 비즈니스 로직이 들어갈 수 있다.
        // 혹은 여러가지 repository 쿼리의 연계는 보통 이곳 service에서 진행 하자.

        return await _itemRepository.GetSimpleItemInfoByItemTId(itemTid);
    }

    public async Task<List<ItemSimpleEntity>> GetItemSimpleInfoListByNameAsync(string name)
    {
        return await _itemRepository.GetItemSimpleInfoByNameList(name);
    }

    public async Task<ItemSimpleEntity?> GetItemSimpleResultByName(string name)
    {
        return await _itemRepository.GetItemSimpleEntityByName(name);
    }


}
