using SHOP.DAL.Models;

namespace SHOP.DAL.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetUserByOrderAsync(int orderId);
        Task<Order?> AddAsync(Order order);
        Task<List<Order>> GetByStatusAsync(OrderStatus status);
        Task<List<Order>> GetOrderByUserAsync(string userId);
        Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus);
        Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId);
    }
}
