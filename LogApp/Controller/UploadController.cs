using Microsoft.AspNetCore.Mvc;
using LogApp.Service;


[Route("upload")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly ImageService _imageService;
    private readonly Logger<UploadController> _logger;

    public UploadController(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = serviceProvider.GetRequiredService<Logger<UploadController>>();
        _imageService = serviceProvider.GetRequiredService<ImageService>();
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

    [HttpPost("multiple")]
    public async Task<IActionResult> UploadMultiple([FromForm] List<IFormFile> files)
    {
        if (files == null || !files.Any())
        {
            return BadRequest("No files received.");
        }

        var uploadResults = new List<string>();

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                try
                {
                    // S3에 업로드
                    var result = await _imageService.UploadFileAsync(file);
                    uploadResults.Add(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to upload file: {file.FileName}");
                    return StatusCode(500, $"Failed to upload file: {file.FileName}");
                }
            }
            else
            {
                _logger.LogWarning($"Empty file received: {file.FileName}");
            }
        }

        return Ok(new { UploadedFiles = uploadResults });

    }
}