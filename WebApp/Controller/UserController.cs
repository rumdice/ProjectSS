using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Service;
using WebApp.ViewModels;
using WepApp.DtoModels;

namespace WebApp.Controller;


[Route("[Controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ItemService _itemService;
    private readonly ILogger<UserController> _logger; // 필요한가?

    public UserController(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<UserController> logger)
    {
        _userService = serviceProvider.GetRequiredService<UserService>();
        _itemService = serviceProvider.GetRequiredService<ItemService>();
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> GetUserInfo(GetUserInfoViewModelRequest request)
    {
        try
        {
            string searchName = "가나다라";
            long searchUserUid = request.UserUid;
            
            var userEntity = await _userService.GetUserInfoByName(searchName);
            if (userEntity == null)
            {
                throw new Exception("db reuslt is empty");
            }

            var userDto = userEntity.EntityToDto();

            _logger.LogInformation("GetUserInfo() API Called");

            return new GetUserInfoViewModelResponse(
                ServiceResponseCode.Success,
                userDto
            ).GetActionResult(this);

        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
            
            // 일반적인 에러에 대한 viewModel 처리
            return ExceptionResponseViewModel.GetActionResult(this, e);
        }
    }


    [HttpPost]
    public async Task<IActionResult> SetUserName(SetUserNameViewModelRequest request)
    {
        try
        {
            await _userService.UpdateUserName(request.UserUid, request.UserName);
            
            return new GetUserInfoViewModelResponse(
                ServiceResponseCode.Success
            ).GetActionResult(this);

        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
            return ExceptionResponseViewModel.GetActionResult(this, e);
        }
    }


    [HttpPost]
    public async Task<IActionResult> AddNewUser(AddNewUserViewModelRequest request)
    {
        try
        {
            // TODO: 유저 키 값은 AI라서 수정 필요 할듯..
            var userDto = new UserDto 
            {
                Id = request.UserUid,
                Name = request.UserName,
                Level = request.UserLevel
            };

            // 컨버터 사용 : TODO: 컨트롤러 및 표현 레이어에서 쓰는게 맞는 걸까?
            await _userService.AddNewUser(userDto.DtoToEntity());
            
            return new GetUserInfoViewModelResponse(
                ServiceResponseCode.Success
            ).GetActionResult(this);

        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
            return ExceptionResponseViewModel.GetActionResult(this, e);
        }
    }


    [HttpPost]
    public async Task<IActionResult> DeleteUser(DeleteUserViewModelRequest request)
    {
        // 유저 삭제시 관련 아이템도 삭제한다고 가정한다.
        try
        {
            // 유저 id 기반으로 유저 정보를 찾는다.
            var userEntity = await _userService.GetUserInfoByUserUid(request.UserUid);
            if (userEntity == null) 
            {
                throw new Exception("Uesr Entity is Null");
            }

            // 유저 id 기반으로 아이템 정보를 찾는다.
            var itemEntityList = await _itemService.GetItemSimpleInfoListByUserIdAsync(request.UserUid);
            
            // 찾은 정보를 넘기기 - 이 구조가 좋은걸까?
            // 아니면 하위 구조에서 아이템 목록을 찾아서 처리 해야 되나?
            // 내부에서 스코프 처리 함.
            await _userService.DeleteUser(userEntity, itemEntityList);

            return new DeleteUserViewModelResponse(
                ServiceResponseCode.Success
            ).GetActionResult(this);
        } 
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
            return ExceptionResponseViewModel.GetActionResult(this, e);
        }

    }

}