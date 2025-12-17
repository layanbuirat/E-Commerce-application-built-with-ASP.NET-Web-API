using SHOP.DAL.Models;

namespace SHOP.DAL.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<bool> UserHasReviewedProductAsync(string userId, int productId);
        Task AddReviewAsync(Review request, string userId);
    }
}
