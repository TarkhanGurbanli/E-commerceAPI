using EcommerceApi.Business.Abstract;
using EcommerceApi.Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("admincategories")]
        public IActionResult CategoryAdminList()
        {
            var result = _categoryService.CategoryAdminCategories();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("homenavbarcategory")]
        public IActionResult HomeNavbarCategory()
        {
            var result = _categoryService.GetNavbarCategories();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("featuredcategory")]
        public IActionResult CategoryFeatured()
        {
            var result = _categoryService.GetFeaturedCategories();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("updatecategory")]
        public IActionResult CategoryUpdate([FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {
            var result = _categoryService.UpdateCategory(categoryUpdateDTO);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("changeStatuscategory/{categoryId}")]
        public IActionResult CategoryChangeStatus(int categoryId)
        {
            var result = _categoryService.CategoryChangeStatus(categoryId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("addcategory")]
        public IActionResult AddCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            var result = _categoryService.AddCategory(categoryCreateDTO);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
        //[HttpPost("AddCategoryWithPhoto")]
        //public IActionResult AddCategoryWithPhoto([FromForm] CategoryCreateDTO categoryCreateDTO, [FromForm] IFormFile photo)
        //{
        //    if (photo == null || photo.Length == 0)
        //    {
        //        return BadRequest("Fotoğraf boş veya geçersiz.");
        //    }

        //    using (var memoryStream = new System.IO.MemoryStream())
        //    {
        //        // IFormFile'ı MemoryStream'e kopyala
        //        photo.CopyTo(memoryStream);

        //        // MemoryStream'den byte dizisini al
        //        byte[] photoData = memoryStream.ToArray();

        //        // ICategoryService üzerinden işlemi gerçekleştir
        //        var result = _categoryService.AddCategoryWithPhoto(categoryCreateDTO, photoData, photo.FileName);

        //        if (result.Success)
        //        {
        //            return Ok(result.Message);
        //        }

        //        return BadRequest(result.Message);
        //    }
        //}

        [HttpDelete("deletecategory/{categoryId}")]
        public IActionResult DeleteCategory([FromRoute] int categoryId)
        {
            var result = _categoryService.DeleteCategory(categoryId);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

    }
}










