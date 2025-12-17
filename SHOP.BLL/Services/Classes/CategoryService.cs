using SHOP.BLL.Services.Interfaces;
using SHOP.DAL.DTO.Requests;
using SHOP.DAL.DTO.Responses;
using SHOP.DAL.Models;
using SHOP.DAL.Repositories.Interfaces;

namespace SHOP.BLL.Services.Classes
{
    public class CategoryService : GenericService<CategoryRequest, CategoryResponses, Category>, ICategoryService
    {
        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
        {

        }

    }
}
