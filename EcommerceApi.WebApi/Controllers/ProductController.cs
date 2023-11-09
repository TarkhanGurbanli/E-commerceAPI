using EcommerceApi.Business.Abstract;
using EcommerceApi.Business.Concrete;
using EcommerceApi.Entities.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductManager> _logger;


        public ProductController(IProductService productService, ILogger<ProductManager> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost("createproduct")]
        public IActionResult CreateProduct([FromBody] ProductCreateDTO productCreateDTO)
        {
            var result = _productService.ProductCreate(productCreateDTO);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("updateproduct")]
        public IActionResult UpdateProduct(ProductUpdateDTO productUpdateDTO)
        {
            var result = _productService.ProductUpdate(productUpdateDTO);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("deleteproduct/{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            var result = _productService.ProductDelete(productId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("productdetail/{productId}")]
        public IActionResult ProductDetail(int productId)
        {
            var result = _productService.GetProductDetail(productId);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        //[HttpGet("testcache/{productId}")]
        //public IActionResult TestCache(int productId)
        //{
        //    _logger.LogInformation($"Testing cache for product ID: {productId}");

        //    var result = _productService.GetProductDetail(productId);

        //    if (result.Success)
        //        return Ok(result);

        //    return BadRequest(result);
        //}

        [HttpGet("featuredproducts")]
        public IActionResult ProductFeatured()
        {
            var product = _productService.GetProductFeaturedList();
            if (product.Success)
                return Ok(product);
            return BadRequest(product);
        }

        [HttpGet("recentproducts")]
        public IActionResult ProductRecent()
        {
            var product = _productService.GetProductRecentList();
            if (product.Success)
                return Ok(product);
            return BadRequest(product);
        }

        [HttpGet("filterproducts")]
        public IActionResult ProductFilter([FromQuery] int categoryId, [FromQuery] int minPrice, [FromQuery] int maxPrice)
        {
            var product = _productService.ProductFilterList(categoryId, minPrice, maxPrice);
            if (product.Success)
                return Ok(product);
            return BadRequest(product);
        }
    }
}
