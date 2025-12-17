using Microsoft.EntityFrameworkCore;
using SHOP.DAL.Data;
using SHOP.DAL.Models;
using SHOP.DAL.Repositories.Interfaces;

namespace SHOP.DAL.Repositories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            context = _context;
        }
        public async Task<Order?> GetUserByOrderAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.User)
                .FirstOrDefaultAsync(O => O.Id == orderId);
        }
        public async Task<Order?> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await _context.Orders.Where(o => o.Status == status)
                .OrderByDescending(O => O.OrderDate).ToListAsync();
        }

        public async Task<List<Order>> GetOrderByUserAsync(string userId)
        {
            return await _context.Orders.Include(o => o.User)
                 .OrderByDescending(O => O.OrderDate).ToListAsync();
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;
            order.Status = newStatus;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId)
        {
            return await _context.Orders.Include(o => o.OrderItems).
                AnyAsync(e => e.UserId == userId && e.Status == OrderStatus.Approved &&
                e.OrderItems.Any(oi => oi.ProductId == productId));

        }

    }
}
