using SHOP.BLL.Services.Interfaces;
using SHOP.DAL.DTO.Requests;
using SHOP.DAL.DTO.Responses;
using SHOP.DAL.Models;
using SHOP.DAL.Repositories.Interfaces;

namespace SHOP.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> AddToCartAsync(CartRequest request, string userId)
        {
            var newItem = new Cart
            {
                ProductId = request.ProductId,
                UserId = userId,
                Count = 1
            };

            return await _cartRepository.AddAsync(newItem) > 0;
        }

        public async Task<CartSummaryResponse> CartSummaryResponseAsync(string userId)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(userId);

            var response = new CartSummaryResponse
            {
                Items = cartItems.Select(ci => new CartResponse
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Count = ci.Count,
                    Price = ci.Product.Price
                }).ToList()
            };

            return response;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            return await _cartRepository.ClearCartAsync(userId);
        }
    }
}
