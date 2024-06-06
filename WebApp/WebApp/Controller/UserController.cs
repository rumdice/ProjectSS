using CoreLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WepApp.DtoModels;

namespace WebApp.Controllers;



[Route("[Controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger; // 필요한가?

    private readonly UserService _userService;

    public UserController(
        ILogger<UserController> logger,
        UserService userService
    )
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost( "[action]" )]
    public async Task<IActionResult> GetUserInfo(GetUserInfoViewModelRequest request)
    {
        try
        {
            string searchName = "가나다라";
            
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


    [HttpPost( "[action]" )]
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


    [HttpPost( "[action]" )]
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

}