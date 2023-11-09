using EcommerceApi.Core.DataAccess.EntityFramework;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.DataAccess.Concrete.EntityFramework
{
    public class EFWishListDAL : EFRepositoryBase<WishList, AppDbContext>, IWishListDAL
    {
        public List<WishList> GetUserWishList(int userId)
        {
            using var context = new AppDbContext();
            var result = context.WishList.Include(x => x.Product).Where(x => x.UserId == userId).ToList();
            return result;
        }
    }
}
