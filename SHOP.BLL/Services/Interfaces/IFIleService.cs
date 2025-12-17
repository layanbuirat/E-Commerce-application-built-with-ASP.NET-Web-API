using Microsoft.AspNetCore.Http;

namespace SHOP.BLL.Services.Interfaces
{
    public interface IFIleService
    {
        Task<string> UploadAsync(IFormFile filePath);

        Task<List<string>> UploadManyAsync(List<IFormFile> files);
    }
}
