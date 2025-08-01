using System.Transactions;
using CoreDB.DBWebApp;
using CoreLibrary;
using CoreLibrary.Repository;
using CoreLibrary.Service;

namespace WebApp.Service;


public class UserService : BaseService
{
    private readonly UserRepository _userRepository;
    private readonly ItemRepository _itemRepository;

    private readonly BaseLogger<UserService> _logger;

    public UserService(
        IServiceProvider serviceProvider)
        : base (serviceProvider)
    {
        _userRepository = serviceProvider.GetRequiredService<UserRepository>();
        _itemRepository = serviceProvider.GetRequiredService<ItemRepository>();
        _logger = serviceProvider.GetRequiredService<BaseLogger<UserService>>();
    }

    public async Task<UserEntity?> GetUserInfoByName(string name)
    {
        return await _userRepository.GetUserInfoByName(name);
    }

    public async Task<UserEntity?> GetUserInfoByUserUid(long userUid)
    {
        return await _userRepository.GetUserInfoByUserId(userUid);
    }

    public async Task UpdateUserName(long userId, string name)
    {
        var userEntity = await _userRepository.GetUserInfoByUserId(userId);
        if (userEntity == null)
        {
            throw new Exception("user Entity is Null");
        }

        userEntity.name = name;

        await _userRepository.UpdateAsync(userEntity);
    }

    public async Task AddNewUser(UserEntity userEntity)
    {   
        await _userRepository.InsertAsync(userEntity);
    }

    public async Task DeleteUser(UserEntity userEntity, IEnumerable<ItemEntity> itemEntity)
    {
        using TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        // 이 사이에서 트렌젝션 작업을 한다. 예를들어 유저를 삭제시 아이템도 삭제한다.
        await _userRepository.DeleteAsync(userEntity);

        // 아이템 엔티티를 찾아서 셋팅하나? 아니면 찾아서 넘거야 하나?
        await _itemRepository.RemoveRangeAsync(itemEntity);

        scope.Complete();
    }

}