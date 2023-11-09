using EcommerceApi.Core.DataAccess;
using EcommerceApi.Entities.Concrete;

namespace EcommerceApi.DataAccess.Abstract
{
    public interface ICategoryDAL : IRepositoryBase<Category>
    {
        //Statusu true olan 10 dene category getir
        List<Category> GetNavbarCategories();
        List<Category> GetFeaturedCategories();
        public string SavePhoto(byte[] photoData, string photoFileName);
    }
}
