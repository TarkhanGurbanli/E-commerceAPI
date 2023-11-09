using EcommerceApi.Core.DataAccess;
using EcommerceApi.Entities.Concrete;

namespace EcommerceApi.DataAccess.Abstract
{
    public interface IUserDAL : IRepositoryBase<User>
    {
        User GetUserOrders(int userId);
    }
}
