using Microsoft.AspNetCore.Identity.UI.Services;
using SHOP.BLL.Services.Classes;
using SHOP.BLL.Services.Interfaces;
using SHOP.DAL.Repositories.Classes;
using SHOP.DAL.Repositories.Interfaces;
using SHOP.DAL.Utils;
using SHOP.PL.Utils;

namespace SHOP.PL
{
    public static class AppConfiguration
    {
        public static void AddConfig(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, BLL.Services.Classes.ProductService>();
            services.AddScoped<ICheckOutService, CheckOutService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ReportService>();
            services.AddScoped<IReviewService, BLL.Services.Classes.ReviewService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFIleService, BLL.Services.Classes.FileService>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ISeedData, SeedData>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmailSender, EmailSetting>();
        }
    }
}
