using System.Transactions;
using CoreDB.DBLogApp;
using CoreLibrary.Repository;
using CoreLibrary.Service;

namespace LogApp.Service;


public class AccountService : BaseService
{
    private readonly AccountRepository _accountRepository;
    private readonly ILogger<AccountService> _logger;

    private readonly Dictionary<string, object> _state = new(); // 로그인 상태 

    public AccountService(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AccountService> logger)
        : base (serviceProvider, httpContextAccessor, logger)
    {
        _accountRepository = serviceProvider.GetRequiredService<AccountRepository>();
        _logger = logger;
        
    }

    public T GetState<T>(string key)
    {
        if (_state.TryGetValue(key, out var value) && value is T tValue)
        {
            return tValue;
        }
        return default!;
    }

    public void SetState<T>(string key, T value)
    {
        _state[key] = value!;
    }

    public async Task<AccountEntity?> GetInfoByName(string name)
    {
        return await _accountRepository.GetByName(name);
    }

    public async Task<AccountEntity?> GetInfoByAUid(string accountId)
    {
        return await _accountRepository.GetById(accountId);
    }

    public async Task<bool> CheckAccountPassword(string accountId, string password)
    {
        var accountInfo = await _accountRepository.GetById(accountId);
        if (accountInfo == null)
        {
            return false;
        }

        return accountInfo.Password == password;
    }

    public async Task UpdateUserPassword(long accountId, string name, string password)
    {
        // var accountEntity = await _userRepository.GetUserInfoByUserId(userId);
        // if (userEntity == null)
        // {
        //     throw new Exception("user Entity is Null");
        // }

        // userEntity.Name = name;

        // await _userRepository.UpdateAsync(userEntity);
    }

    public async Task AddNewAccount(AccountEntity accountEntity)
    {   
        await _accountRepository.InsertAsync(accountEntity);
    }

    public async Task DeleteAccount(AccountEntity accountEntity)
    {
        await _accountRepository.DeleteAsync(accountEntity);
    }

}