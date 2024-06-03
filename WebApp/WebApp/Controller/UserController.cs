using CoreLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

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

            // TODO: 컨버터 사용 (Entity to Dto)
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
}