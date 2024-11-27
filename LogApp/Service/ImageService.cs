using CoreLibrary.Database;
using CoreLibrary.Repository;
using CoreLibrary.Service;

//inject IHttpClientFactory HttpClientFactory

// Service : 비즈니스 로직을 처리하는 단계. 간단히 자료를 가져오는 것 부터 복잡한 쿼리 연계까지

namespace LogApp.Service;


public class ImageService : BaseService
{
    private readonly ItemRepository _itemRepository;
    private readonly ILogger<ImageService> _logger;

    public List<string> ImageUrls { get; private set; } = new();
    public bool IsLoading { get; private set; } = false;
    public string? ErrorMessage { get; private set; }

    public ImageService( 
        IServiceProvider serviceProvider,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ImageService> logger)
        : base (serviceProvider, httpContextAccessor, logger)
    {
        _itemRepository = serviceProvider.GetService<ItemRepository>();
        _logger = logger;

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
            ImageUrls.Add(imageUrl);
            
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

}
