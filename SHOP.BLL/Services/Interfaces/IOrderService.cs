using SHOP.DAL.Models;

namespace SHOP.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetUserByOrderAsync(int orderId);
        Task<Order?> AddOrderAsync(Order order);
        Task<List<Order>> GetByStatusAsync(OrderStatus status);
        Task<List<Order>> GetOrderByUserAsync(string userId);
        Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus);
    }
}
