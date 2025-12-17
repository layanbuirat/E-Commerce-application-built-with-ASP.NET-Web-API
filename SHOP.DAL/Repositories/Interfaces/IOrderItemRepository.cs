using SHOP.DAL.Models;

namespace SHOP.DAL.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddRangeAsync(List<OrderItem> orderItem);
    }
}
