using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLibrary.Service;
using CoreLibrary.Database;
using CoreLibrary.Repository;
using Microsoft.Extensions.Configuration;

//inject IHttpClientFactory HttpClientFactory

// Service : 비즈니스 로직을 처리하는 단계. 간단히 자료를 가져오는 것 부터 복잡한 쿼리 연계까지

namespace LogApp.Service;

public class ImageService : BaseService
{
    private readonly ItemRepository _itemRepository;
    private readonly ILogger<ImageService> _logger;
    private IAmazonS3 _s3Client {get; set; }  // S3 클라이언트 추가
    private readonly IConfiguration _configuration;

    public List<string> ImageUrls { get; private set; } = new();
    public bool IsLoading { get; private set; } = false;
    public string? ErrorMessage { get; private set; }

    private readonly RegionEndpoint _region = RegionEndpoint.APNortheast2;

    public ImageService(
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ImageService> logger,
        IAmazonS3 s3Client,
        IConfiguration configuration)
        : base(serviceProvider, httpContextAccessor, logger)
    {
        _itemRepository = serviceProvider.GetService<ItemRepository>();
        _logger = logger;
        _s3Client = s3Client;
        _configuration = configuration;

    }

    public async Task<List<ItemSimpleEntity>> GetItemSimpleInfoListByNameAsync(string name)
    {
        return await _itemRepository.GetItemSimpleInfoByNameList(name);
    }

    public async Task<ItemSimpleEntity?> GetItemSimpleResultByName(string name)
    {
        return await _itemRepository.GetItemSimpleEntityByName(name);
    }

    public async Task LoadImages()
    {
        IsLoading = true;
        ErrorMessage = null;
        ImageUrls.Clear();

        try
        {
            var imageUrl = "https://rumdice-project-ss.s3.ap-northeast-1.amazonaws.com/images/bike.jpeg";
            var imageUrl2 = "https://rumdice-project-ss.s3.ap-northeast-1.amazonaws.com/images/light.jpeg";
            var imageUrl3 = "https://rumdice-project-ss.s3.ap-northeast-1.amazonaws.com/images/river.jpeg";
            var imageUrl4 = "https://rumdice-project-ss.s3.ap-northeast-1.amazonaws.com/images/tower.jpeg";
            var imageUrl5 = "https://rumdice-project-ss.s3.ap-northeast-1.amazonaws.com/images/cake.jpeg";
            var imageUrl6 = "https://rumdice-project-ss.s3.ap-northeast-1.amazonaws.com/images/river2.jpeg";
            var imageUrl7 = "https://rumdice-project-ss.s3.ap-northeast-1.amazonaws.com/images/latke1.jpeg";
            var imageUrl8 = "https://rumdice-project-ss.s3.ap-northeast-1.amazonaws.com/images/lake2.jpeg";
            var imageUrl9 = "https://rumdice-project-ss.s3.ap-northeast-1.amazonaws.com/images/me.jpeg";


            ImageUrls.Add(imageUrl);
            ImageUrls.Add(imageUrl2);
            ImageUrls.Add(imageUrl3);
            ImageUrls.Add(imageUrl4);
            ImageUrls.Add(imageUrl5);
            ImageUrls.Add(imageUrl6);
            ImageUrls.Add(imageUrl7);
            ImageUrls.Add(imageUrl8);
            ImageUrls.Add(imageUrl9);



            //이미지를 보내주는 api 서버 통신인데 일단 s3으로 대체 
            // var client = HttpClientFactory.CreateClient();
            // var response = await client.GetAsync(imageUrl);

            // if (response.IsSuccessStatusCode)
            // {
            //     var images = await response.Content.ReadFromJsonAsync<List<string>>();
            //     if (images != null)
            //     {
            //         imageUrls.AddRange(images);
            //     }
            // }
            // else
            // {
            //     errorMessage = $"Failed to load images: {response.StatusCode} - {response.ReasonPhrase}";
            // }

        }
        catch (Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }


    public async Task LoadImagesByS3()
    {
        IsLoading = true;
        ErrorMessage = null;
        ImageUrls.Clear();

        var credentials = new BasicAWSCredentials(
            _configuration["AWS:AccessKey"], 
            _configuration["AWS:SecretKey"]);

        var clientConfig = new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.GetBySystemName(_configuration["AWS:Region"])
        };

        _s3Client = new AmazonS3Client(credentials, clientConfig);

        try
        {
            var bucketName = "rumdice-projectss";  // S3 버킷 이름
            var request = new ListObjectsV2Request
            {
                BucketName = bucketName, // S3 버킷 이름
                Prefix = "images/"              // 원하는 디렉터리 경로(필요하지 않다면 제거 가능)
            };

            // S3에서 객체 목록을 가져옵니다.
            var response = await _s3Client.ListObjectsV2Async(request);
            // 성공 여부 확인은 따로 필요 없음. 예외 처리에서 실패 시 캐치됨.
            foreach (var s3Object in response.S3Objects)
            {
                // S3 객체의 URL 생성
                var imageUrl = $"https://{request.BucketName}.s3.{_region.SystemName}.amazonaws.com/{s3Object.Key}";
                ImageUrls.Add(imageUrl);
            }
        }
        catch (AmazonS3Exception ex)
        {
            ErrorMessage = $"AWS S3 Error: {ex.Message}";
            _logger.LogError(ex, "Error accessing S3");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

}
