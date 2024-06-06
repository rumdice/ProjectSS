



using CoreLibrary.Database;

public class UserService
{
    // TODO: 경우에 따라선 서비스가 여러가지 종류의 repository 를 들고 있을 수 있다.
    private readonly UserRepository _userRepository;
    private readonly ItemRepository _itemRepository;

    private readonly ILogger<UserService> _logger;

    public UserService(
        UserRepository userRepository,
        ItemRepository itemRepository,
        ILogger<UserService> logger
    )
    {
        _userRepository = userRepository;
        _itemRepository = itemRepository;
        _logger = logger;
    }

    public async Task<UserEntity?> GetUserInfoByName(string name)
    {
        return await _userRepository.GetUserInfoByName(name);
    }

    public async Task UpdateUserName(long userId, string name)
    {
        var userEntity = await _userRepository.GetUserInfoByUserId(userId);
        
        if (userEntity == null)
        {
            throw new Exception("user Entity is Null");
        }

        // 데이터를 업데이트 한다 엔티티의 값을 변경
        userEntity.Name = name;

        // DB에 적용 한다.
        await _userRepository.UpdateAsync(userEntity);
    }

    public async Task AddNewUser(UserEntity userEntity)
    {   
        await _userRepository.InsertAsync(userEntity);
    }

}