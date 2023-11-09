using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Entities.DTOs.CategoryDTOs;

namespace EcommerceApi.Business.Abstract
{
    public interface ICategoryService
    {
        IResult AddCategory(CategoryCreateDTO categoryCreateDTO);
        IResult UpdateCategory(CategoryUpdateDTO categoryUpdateDTO);
        IResult DeleteCategory(int categoryId);
        IResult CategoryChangeStatus(int categoryId);
        IResult AddCategoryWithPhoto(CategoryCreateDTO categoryCreateDTO, byte[] photoData, string photoFileName);
        //Admin ucun butun Categoryleri getirmek
        IDataResult<List<CategoryAdminListDTO>> CategoryAdminCategories();
        IDataResult<List<CategoryHomeNavBarDTO>> GetNavbarCategories();
        IDataResult<List<CategoryFeaturedDTO>> GetFeaturedCategories();
    }
}
