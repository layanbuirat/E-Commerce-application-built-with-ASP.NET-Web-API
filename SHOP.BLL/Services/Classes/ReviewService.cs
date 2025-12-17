using Mapster;
using SHOP.BLL.Services.Interfaces;
using SHOP.DAL.DTO.Requests;
using SHOP.DAL.Models;
using SHOP.DAL.Repositories.Interfaces;

namespace SHOP.BLL.Services.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IOrderRepository orderRepository, IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<bool> AddReviewAsync(ReviewRequest reviewRequest, string userId)
        {
            var hasOrder = await _orderRepository.UserHasApprovedOrderForProductAsync(userId, reviewRequest.ProductId);

            if (!hasOrder) return false;

            var alreadyReviews = await _reviewRepository.UserHasReviewedProductAsync(userId, reviewRequest.ProductId);
            if (alreadyReviews) return false;

            var review = reviewRequest.Adapt<Review>();

            await _reviewRepository.AddReviewAsync(review, userId);
            return true;
        }
    }
}
