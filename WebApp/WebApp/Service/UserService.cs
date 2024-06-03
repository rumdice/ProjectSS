



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

}