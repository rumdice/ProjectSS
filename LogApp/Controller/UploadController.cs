using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

[Route("upload")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public UploadController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost("single")]
    public async Task<IActionResult> UploadSingle(IFormFile file)
    {
        Console.WriteLine("Call!!!!");

        if (file == null || file.Length == 0)
        {
            return BadRequest("No file received.");
        }

        try
        {
            // 파일 저장 경로 설정 (예: wwwroot/uploads)
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // 고유 파일명 생성
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // 파일 저장
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 업로드된 파일 URL 생성
            var fileUrl = $"/uploads/{uniqueFileName}";
            return Ok(new { Url = fileUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}