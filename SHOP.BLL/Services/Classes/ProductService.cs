using Mapster;
using Microsoft.AspNetCore.Http;
using SHOP.BLL.Services.Interfaces;
using SHOP.DAL.DTO.Requests;
using SHOP.DAL.DTO.Responses;
using SHOP.DAL.Models;
using SHOP.DAL.Repositories.Interfaces;

namespace SHOP.BLL.Services.Classes
{
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFIleService _fileService;
        public ProductService(IProductRepository productRepository, IFIleService fileService) : base(productRepository)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<int> CreateProductAsync(ProductRequest request)
        {
            var entity = request.Adapt<Product>();
            entity.CreatedAt = DateTime.UtcNow;

            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;
            }

            if (request.SubImages != null)
            {
                var subImagesPaths = await _fileService.UploadManyAsync(request.SubImages);
                entity.SubImages = subImagesPaths.Select(img => new ProductImage { ImageName = img }).ToList();
            }

            return _productRepository.Add(entity);
        }


        public async Task<List<ProductResponse>> GetAllProducts(HttpRequest request, bool onlayActive = false, int pageNumber = 1, int pageSize = 1)
        {
            var products = _productRepository.GetAllProductsWithImage();
            if (onlayActive)
            {
                products = products.Where(p => p.status == Status.Active).ToList();
            }

            var pagedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return pagedProducts.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Quantity = p.Quantity,
                MainImage = $"{request.Scheme}://{request.Host}/Images/{p.MainImage}",
                SubImagesUrls = p.SubImages.Select(img => $"{request.Scheme}://{request.Host}/Images/{img.ImageName}").ToList(),
                Reviews = p.Reviews.Select(r => new ReviewResponse
                {
                    Id = r.Id,
                    FullName = r.User.FullName,
                    Rate = r.Rate,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate
                }).ToList()
            }).ToList();
        }
    }

}
