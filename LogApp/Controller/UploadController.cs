using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using LogApp.Service;
using System;
using CoreLibrary.Controller;


[Route("upload")]
[ApiController]
public class UploadController : BaseController
{
    private readonly IWebHostEnvironment _environment;
    private readonly ImageService _imageService;

    public UploadController(
        IWebHostEnvironment environment,
        ImageService imageService)
        
    {
        _environment = environment;
        _imageService = imageService;
    }

    [HttpPost("single")]
    public async Task<IActionResult> UploadSingle(IFormFile file)
    {
        Console.WriteLine("Call!!!!");

        if (file == null || file.Length == 0)
        {
            return BadRequest("No file received.");
        }

            // S3에 업로드
        _imageService.UploadFileAsync(file);
        return Ok();
     

        // 로컬에 로직 복사하는것 수정
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

           

            // 결과값은 리턴 (추후 삭제)
            return Ok(new
            {
                Uploaded = true,
                FileName = file.FileName,
                Url = fileUrl
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}