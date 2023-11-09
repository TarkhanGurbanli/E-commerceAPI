using EcommerceApi.Core.DataAccess;
using EcommerceApi.Entities.Concrete;
using EcommerceApi.Entities.DTOs.ProductDTOs;

namespace EcommerceApi.DataAccess.Abstract
{
    public interface IProductDAL : IRepositoryBase<Product>
    {
        Product GetProduct(int id);
        List<Product> GetFeaturedProducts();
        List<Product> GetRecentProducts();
        void RemoveProductCount(List<ProductDecrementQuantityDTO> productDecrementQuantityDTOs);
    }
}
