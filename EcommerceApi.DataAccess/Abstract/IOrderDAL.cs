using EcommerceApi.Core.DataAccess;
using EcommerceApi.Entities.Concrete;

namespace EcommerceApi.DataAccess.Abstract
{
    public interface IOrderDAL : IRepositoryBase<Order>
    {
        void AddRange(int userId, List<Order> orders);
        List<Order> GetOrderByUser(int userId);
    }
}
