using AutoMapper;
using EcommerceApi.Business.Abstract;
using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Core.Utilities.Result.Concrete.ErrorResult;
using EcommerceApi.Core.Utilities.Result.Concrete.SuccessResult;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;
using EcommerceApi.Entities.DTOs.CategoryDTOs;

namespace EcommerceApi.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDAL categoryDAL, IMapper mapper)
        {
            _categoryDAL = categoryDAL ?? throw new ArgumentNullException(nameof(categoryDAL));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IResult AddCategory(CategoryCreateDTO categoryCreateDTO)
        {
            try
            {
                if (categoryCreateDTO == null)
                    throw new ArgumentNullException(nameof(categoryCreateDTO), "CategoryCreateDTO cannot be null");

                // Eyni adda Category varmi deye yoxlayir
                if (_categoryDAL.Any(x => x.CategoryName == categoryCreateDTO.CategoryName))
                    return new ErrorResult("A category with the same name already exists.");

                var mappedCategory = _mapper.Map<Category>(categoryCreateDTO);
                mappedCategory.Status = true;
                mappedCategory.CreatedDate = DateTime.Now;

                _categoryDAL.Add(mappedCategory);

                return new SuccessResult("Category added successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"An error occurred while adding the category: {ex.Message}");
            }
        }

        public IResult AddCategoryWithPhoto(CategoryCreateDTO categoryCreateDTO, byte[] photoData, string photoFileName)
        {
            try
            {
                // Validation and existing category check code...

                // Map CategoryCreateDTO to Category
                var mappedCategory = _mapper.Map<Category>(categoryCreateDTO);

                // Set additional properties (e.g., photo)
                mappedCategory.Status = true;
                mappedCategory.CreatedDate = DateTime.Now;
                mappedCategory.PhotoUrl = _categoryDAL.SavePhoto(photoData, photoFileName); // Save the photo and get the URL

                // Add the category to the database
                _categoryDAL.Add(mappedCategory);

                return new SuccessResult("Category added successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"An error occurred while adding the category: {ex.Message}");
            }
        }




        public IDataResult<List<CategoryAdminListDTO>> CategoryAdminCategories()
        {
            try
            {
                var categories = _categoryDAL.GetAll();
                var mappedCategories = _mapper.Map<List<CategoryAdminListDTO>>(categories);

                return new SuccessDataResult<List<CategoryAdminListDTO>>(mappedCategories);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryAdminListDTO>>($"An error occurred while getting category list: {ex.Message}");
            }
        }

        public IResult CategoryChangeStatus(int categoryId)
        {
            try
            {
                var category = _categoryDAL.Get(x => x.Id == categoryId);
                if (category == null)
                    throw new NullReferenceException($"Category with ID {categoryId} not found");

                category.Status = !category.Status;

                _categoryDAL.Update(category);

                return new SuccessResult("Changed Category Status!");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"An error occurred while changing category status: {ex.Message}");
            }
        }

        public IResult DeleteCategory(int categoryId)
        {
            try
            {
                var category = _categoryDAL.Get(x => x.Id == categoryId);
                if (category == null)
                    throw new NullReferenceException($"Category with ID {categoryId} not found");

                _categoryDAL.Delete(category);

                return new SuccessResult("Category Deleted Successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"An error occurred while deleting the category: {ex.Message}");
            }
        }

        public IDataResult<List<CategoryFeaturedDTO>> GetFeaturedCategories()
        {
            try
            {
                // Öne çıxan kategorileri cagirma işlemi
                var categories = _categoryDAL.GetFeaturedCategories();

                // Null kontrolü
                if (categories == null || !categories.Any())
                    return new ErrorDataResult<List<CategoryFeaturedDTO>>("No featured categories found.");

                // DTO'ya dönüştürme işlemi
                var mappedCategories = _mapper.Map<List<CategoryFeaturedDTO>>(categories);

                // Başarı durumuyla sonuç döndürme
                return new SuccessDataResult<List<CategoryFeaturedDTO>>(mappedCategories);
            }
            catch (Exception ex)
            {
                // Hata durumunda uygun şekilde işlem yapma
                return new ErrorDataResult<List<CategoryFeaturedDTO>>($"An error occurred while retrieving featured categories: {ex.Message}");
            }
        }


        public IDataResult<List<CategoryHomeNavBarDTO>> GetNavbarCategories()
        {
            try
            {
                var categories = _categoryDAL.GetNavbarCategories();
                var mappedCategories = _mapper.Map<List<CategoryHomeNavBarDTO>>(categories);

                return new SuccessDataResult<List<CategoryHomeNavBarDTO>>(mappedCategories);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryHomeNavBarDTO>>($"An error occurred while getting navbar categories: {ex.Message}");
            }
        }

        public IResult UpdateCategory(CategoryUpdateDTO categoryUpdateDTO)
        {
            try
            {
                var category = _categoryDAL.Get(x => x.Id == categoryUpdateDTO.Id);
                if (category == null)
                    throw new NullReferenceException($"Category with ID {categoryUpdateDTO.Id} not found");

                if (_categoryDAL.Any(x => x.CategoryName == categoryUpdateDTO.CategoryName))
                    return new ErrorResult("A category with the same name already exists.");
                var mappedCategory = _mapper.Map<Category>(categoryUpdateDTO);

                category.PhotoUrl = mappedCategory.PhotoUrl;
                category.CategoryName = mappedCategory.CategoryName;

                _categoryDAL.Update(category);

                return new SuccessResult("Category Updated!");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"An error occurred while updating the category: {ex.Message}");
            }
        }
    }
}
