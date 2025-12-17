using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SHOP.BLL.Services.Interfaces;
using SHOP.DAL.DTO.Requests;

namespace SHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductsController(IProductService brandService)
        {
            _productService = brandService;
        }

        [HttpGet("")]
        public IActionResult GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var result = _productService.GetAllProducts(Request, false, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
            var result = await _productService.CreateProductAsync(request);
            return Ok(result);
        }


    }
}
