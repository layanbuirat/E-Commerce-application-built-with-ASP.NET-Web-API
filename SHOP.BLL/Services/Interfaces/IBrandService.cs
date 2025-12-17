using SHOP.DAL.DTO.Requests;
using SHOP.DAL.DTO.Responses;
using SHOP.DAL.Models;

namespace SHOP.BLL.Services.Interfaces
{
    public interface IBrandService : IGenericService<BrandRequest, BrandResponses, Brand>
    {
        Task<int> CreateFileAsync(BrandRequest request);
    }
}
