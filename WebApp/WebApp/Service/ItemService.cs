using Microsoft.AspNetCore.Mvc;
using WebApp.Database;

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

    // 비즈니스 로직을 처리하는 단계. 간단히 자료를 가져오는 것 부터 복잡한 쿼리 연계까지
    // TODO : 결과값 은 어떤 자료형이 맞는가?
    public async Task<ItemSimpleEntity?> GetSimpleItemResultAsync(long itemTid)
    {
        var itemSimpleEntity = await _itemRepository.GetSimpleItemInfoByItemTId(itemTid);
        if (itemSimpleEntity == null)
        {
            _logger.LogInformation("itemRepo is NULL!"); // 의도한대로 동작은 확인
            return null; 
            // TODO: 에러처리를 하고 예외를 던지는게 좋을까?
            // 에러처리를 일관되게 하고 싶음.
        }

        return itemSimpleEntity; 
    }
}
