using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Entities.DTOs.ProductDTOs;

namespace EcommerceApi.Business.Abstract
{
    public interface IProductService
    {
        IResult ProductCreate(ProductCreateDTO productCreateDTO);
        IResult ProductUpdate(ProductUpdateDTO productUpdateDTO);
        IResult ProductDelete(int productId);
        IResult RemoveProductCount(List<ProductDecrementQuantityDTO> productDecrementQuantityDTOs);
        IDataResult<ProductDetailDTO> GetProductDetail(int productId);
        IDataResult<List<ProductFeaturedDTO>> GetProductFeaturedList();
        IDataResult<List<ProductRecentDTO>> GetProductRecentList();
        IDataResult<List<ProductFilterDTO>> ProductFilterList(int categoryId, int minPrice, int maxPrice);
        IDataResult<bool> CheckProductCount(List<int> productIds);
    }
}
