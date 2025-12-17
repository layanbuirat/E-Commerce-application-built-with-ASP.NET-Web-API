using SHOP.DAL.Models;

namespace SHOP.DAL.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task DecreaseQuantityAsync(List<(int productId, int quantity)> items);
        List<Product> GetAllProductsWithImage();
    }
}
