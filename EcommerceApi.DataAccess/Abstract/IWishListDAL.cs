using EcommerceApi.Core.DataAccess;
using EcommerceApi.Entities.Concrete;

namespace EcommerceApi.DataAccess.Abstract
{
    public interface IWishListDAL : IRepositoryBase<WishList>
    {
        List<WishList> GetUserWishList(int userId);
    }
}
