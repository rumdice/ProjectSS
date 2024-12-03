using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using LogApp.Service;
using System;
using CoreLibrary.Controller;


[Route("upload")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly ImageService _imageService;
    private readonly ILogger<UploadController> _logger;

    public UploadController(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<UploadController> logger)
    {
        _imageService = serviceProvider.GetRequiredService<ImageService>();
        _logger = logger;
    }

    [HttpPost("single")]
    public async Task<IActionResult> UploadSingle(IFormFile file)
    {

        if (file == null || file.Length == 0)
        {
            return BadRequest("No file received.");
        }

        // S3에 업로드
        await _imageService.UploadFileAsync(file);
        return Ok();
    }
}