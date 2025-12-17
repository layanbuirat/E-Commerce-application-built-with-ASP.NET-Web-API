using Microsoft.EntityFrameworkCore;
using SHOP.DAL.Data;
using SHOP.DAL.Models;
using SHOP.DAL.Repositories.Interfaces;

namespace SHOP.DAL.Repositories.Classes
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DecreaseQuantityAsync(List<(int productId, int quantity)> items)
        {
            var productIds = items.Select(i => i.productId).ToList();

            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            foreach (var product in products)
            {
                var item = items.First(i => i.productId == product.Id);
                if (product.Quantity < item.quantity)
                {
                    throw new Exception($"Not enough quantity in stock for product {product.Name}");
                }
                product.Quantity -= item.quantity;
            }
            await _context.SaveChangesAsync();

        }

        public List<Product> GetAllProductsWithImage()
        {
            return _context.Products.Include(p => p.SubImages).Include(p => p.Reviews).ThenInclude(r => r.User).ToList();
        }
    }
}
