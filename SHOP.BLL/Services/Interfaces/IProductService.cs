using Microsoft.AspNetCore.Http;
using SHOP.DAL.DTO.Requests;
using SHOP.DAL.DTO.Responses;
using SHOP.DAL.Models;

namespace SHOP.BLL.Services.Interfaces
{
    public interface IProductService : IGenericService<ProductRequest, ProductResponse, Product>
    {
        Task<int> CreateProductAsync(ProductRequest request);
        Task<List<ProductResponse>> GetAllProducts(HttpRequest request, bool onlayActive = false, int pageNumber = 1, int pageSize = 1);
    }
}
