using EcommerceApi.Core.DataAccess.EntityFramework;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.DataAccess.Concrete.EntityFramework
{
    public class EFUserDAL : EFRepositoryBase<User, AppDbContext>, IUserDAL
    {
        public User GetUserOrders(int userId)
        {
            using var context = new AppDbContext();

            var user = context.Users
                .Include(x => x.Orders)
                    .ThenInclude(o => o.Product)
                .FirstOrDefault(x => x.Id == userId);

            return user;
        }
    }
}
