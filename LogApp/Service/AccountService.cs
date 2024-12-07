using System.Transactions;
using CoreDB.DBLogApp;
using CoreLibrary.Repository;
using CoreLibrary.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LogApp.Service;


public class AccountService : BaseService
{
    private readonly NavigationManager _navigation;
    private readonly AccountRepository _accountRepository;
    private readonly ILogger<AccountService> _logger;

    private readonly Dictionary<string, object> _state = new(); // 로그인 상태 

    public AccountService(
        IServiceProvider serviceProvider,
        ILogger<AccountService> logger,
        NavigationManager navigation)
        : base (serviceProvider, logger)
    {
        _accountRepository = serviceProvider.GetRequiredService<AccountRepository>();
        _logger = logger;
        _navigation = navigation;
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

    public void EnsureAuthenticated()
    {
        bool isAuthenticated = this.GetState<bool>("IsAuthenticated");
        if (!isAuthenticated)
        {
            _navigation.NavigateTo("/login");
        }

        // 로거 기반의 로깅 시스템
        //_logger.LogInformation("EnsureAuthenticated()!!!!!!!!");
    }

    public async Task<List<AccountEntity>> GetInfoAll()
    {
        return await _accountRepository.GetAll();
    }

    public async Task<AccountEntity?> GetInfoByName(string name)
    {
        return await _accountRepository.GetByName(name);
    }

    public async Task<bool> IsExistAccount(string name)
    {
        var accountInfo = await _accountRepository.GetByName(name);
        if (accountInfo == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> CheckAccountPassword(string name, string password)
    {
        var accountInfo = await _accountRepository.GetByName(name);
        if (accountInfo == null)
        {
            return false;
        }

        return accountInfo.password == password;
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

    public async Task AddNewAccount(string _name, string _password)
    {
        var accountEntity = new AccountEntity
        {
            name = _name,
            password = _password
        };

        await _accountRepository.InsertAsync(accountEntity);
    }

    public async Task DeleteAccount(AccountEntity accountEntity)
    {
        await _accountRepository.DeleteAsync(accountEntity);
    }

}