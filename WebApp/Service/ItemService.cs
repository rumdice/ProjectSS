using CoreDB.DBWebApp;
using CoreLibrary;
using CoreLibrary.Repository;
using CoreLibrary.Service;

// Service : 비즈니스 로직을 처리하는 단계. 간단히 자료를 가져오는 것 부터 복잡한 쿼리 연계까지

namespace WebApp.Service;


public class ItemService : BaseService
{
    private readonly ItemRepository _itemRepository;
    private readonly BaseLogger<ItemService> _logger;

    public ItemService( 
        IServiceProvider serviceProvider)
        : base (serviceProvider)
    {
        _itemRepository = serviceProvider.GetRequiredService<ItemRepository>();
        _logger = serviceProvider.GetRequiredService<BaseLogger<ItemService>>();
    }

    public virtual async Task<ItemEntity?> GetSimpleItemResultAsync(long itemTid)
    {
        // 서비스의 역활 - repo를 사용할 뿐 그 이상의 비즈니스 로직 수행 필요하다.
        return await _itemRepository.GetSimpleItemInfoByItemTId(itemTid);
    }

    public async Task<List<ItemEntity>> GetItemSimpleInfoListByNameAsync(string name)
    {
        return await _itemRepository.GetItemSimpleInfoByNameList(name);
    }

    public async Task<List<ItemEntity>> GetItemSimpleInfoListByUserIdAsync(long userUid)
    {
        return await _itemRepository.GetItemSimpleInfoListByUserId(userUid);
    }

    public async Task<ItemEntity?> GetItemSimpleResultByName(string name)
    {
        return await _itemRepository.GetItemSimpleEntityByName(name);
    }
}
