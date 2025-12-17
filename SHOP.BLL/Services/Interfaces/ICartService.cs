using SHOP.DAL.DTO.Requests;
using SHOP.DAL.DTO.Responses;

namespace SHOP.BLL.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(CartRequest request, string userId);

        Task<CartSummaryResponse> CartSummaryResponseAsync(string userId);

        Task<bool> ClearCartAsync(string userId);
    }
}
