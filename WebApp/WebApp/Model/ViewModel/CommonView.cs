using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ViewModels;

// TODO: coreSystem 혹은 상위 클래스로 이동시키기?

/// <summary>
/// 일반적인 에러코드. 성공. 실패
/// </summary>
public enum ServiceResponseCode
{
    InternalError = -1,
	None = 0, 
	Success = 1, 
	BadRequest = 2, 
	PermissionDenied = 3, 
	NoChanges = 4, 
	AccessDenied = 5, 
	InvalidUserId = 6,

    InvatildItemIid = 7,
}

/// <summary>
/// 일반적인 ResponseViewModel
/// </summary>
public class CommonResponseViewModel
{
    [JsonPropertyName("code")]
    public int code { get; set; }

    [JsonPropertyName("desc")]
    public string? desc { get; set; }

    [JsonPropertyName("error")]
    public string? error { get; set; }

    public CommonResponseViewModel()
    {
    }
}


/// <summary>
/// API response 기본 모델
/// </summary>
/// <typeparam name="T"></typeparam>
public class CodeResponseViewModel<T> : CommonResponseViewModel where T: Enum
{
    [JsonIgnore]
    public T? ResponesCode
    {
        get
        {
            if(code < 0) {
                return default(T);
            }

            return (T)Enum.ToObject(typeof(T), code);
        }
    }

    [JsonConstructor]
    public CodeResponseViewModel() {}

    public CodeResponseViewModel(T code)
    {

        this.code = Convert.ToInt32(code);
        this.desc = code.ToString();
    }

    public CodeResponseViewModel(ServiceResponseCode code, Exception exceoption)
    {
        this.code = (int)code;
        this.desc = "Exception";
        this.error = exceoption.Message;
    }

    public IActionResult GetActionResult(ControllerBase controller)
    {
        return controller.Ok(this);
    }
}


/// <summary>
/// 에러발생시 기본에러 viewModel
/// </summary>
public class ExceptionResponseViewModel
{
    public static IActionResult GetActionResult(ControllerBase controller, Exception exception)
    {
        return controller.Ok(new {
            code = -1,
            desc = "Exception",
            error = exception.Message
        });
    }

    public static IActionResult GetActionResult<T>(T code, Exception exception) where T : Enum
    {
        var response = new CommonResponseViewModel
        {
            code = Convert.ToInt32(code),
            desc = nameof( Exception ),
            error = exception.Message
        };

        return new OkObjectResult( response );
    }
}


