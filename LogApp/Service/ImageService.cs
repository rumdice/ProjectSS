using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using CoreLibrary.Service;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace LogApp.Service;

public class ImageService : BaseService
{
    private IAmazonS3 _s3Client { get; set; }  // S3 클라이언트 추가

    public List<string> ImageUrls { get; private set; } = new();
    public bool IsLoading { get; private set; } = false;
    public string? ErrorMessage { get; private set; }

    private readonly RegionEndpoint _region = RegionEndpoint.APNortheast2;
    private readonly Logger<ImageService> _logger;

    public ImageService(
        IServiceProvider serviceProvider,
        IAmazonS3 s3Client,
        IConfiguration configuration)
        : base(serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<Logger<ImageService>>();
        _s3Client = s3Client;
    }

    ///
    /// AWS Scrent Manager 에서 정해진 값을 가져온다.
    ///
    private static async Task<BasicAWSCredentials> GetSecret()
    {
        string secretName = "rumdice-aws-acesss-key";
        
        IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.APNortheast2);

        GetSecretValueRequest request = new GetSecretValueRequest
        {
            SecretId = secretName,
            VersionStage = "AWSCURRENT",
        };

        GetSecretValueResponse response;

        try
        {
            response = await client.GetSecretValueAsync(request);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        // JSON 파싱
        var secret = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(response.SecretString);

        if (secret == null ||
            !secret.ContainsKey("aws_access_key_id") ||
            !secret.ContainsKey("aws_secret_access_key"))
        {
            throw new Exception("Secrets JSON is missing required keys.");
        }

        // AWS Credentials 생성
        return new BasicAWSCredentials(secret["aws_access_key_id"], secret["aws_secret_access_key"]);
    }


    public async Task LoadImagesByS3()
    {
        IsLoading = true;
        ErrorMessage = "";
        ImageUrls.Clear();

        // 가져온 엑세스 키와 시크릿 키로 s3 접근
        var credentials = await ImageService.GetSecret();
        var clientConfig = new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.APNortheast2
        };

        _s3Client = new AmazonS3Client(credentials, clientConfig);

        try
        {
            var request = new ListObjectsV2Request
            {
                BucketName = "rumdice-projectss",   // S3 버킷 이름
                Prefix = "images/"                  // 원하는 디렉터리 경로(필요하지 않다면 제거 가능)
            };

            // S3에서 객체 목록을 가져옵니다.
            var response = await _s3Client.ListObjectsV2Async(request);
            // 성공 여부 확인은 따로 필요 없음. 예외 처리에서 실패 시 캐치됨.
            foreach (var s3Object in response.S3Objects)
            {
                // S3 객체의 URL 생성
                var imageUrl = $"https://{request.BucketName}.s3.{_region.SystemName}.amazonaws.com/{s3Object.Key}";

                var imageName = s3Object.Key.Split("/");
                if (string.IsNullOrEmpty(imageName[1]))
                    continue;

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

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        try
        {
            var keyName = $"images/{Guid.NewGuid()}_{file.Name}.jpeg";

            // 스트림 관리
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0; // 스트림 포인터 초기화

                var request = new PutObjectRequest
                {
                    BucketName = "rumdice-projectss",
                    Key = keyName,
                    InputStream = memoryStream,
                    ContentType = file.ContentType,
                };

                var response = await _s3Client.PutObjectAsync(request);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return $"https://{request.BucketName}.s3.{RegionEndpoint.APNortheast2.SystemName}.amazonaws.com/{keyName}";
                }
                else
                {
                    throw new Exception($"Failed to upload file. HTTP {response.HttpStatusCode}");
                }
            }
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine($"Error encountered: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        return "";
    }

}
