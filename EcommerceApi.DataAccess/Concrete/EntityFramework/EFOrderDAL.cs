using EcommerceApi.Core.DataAccess.EntityFramework;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;
using EcommerceApi.Entities.Concrete.Enums;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.DataAccess.Concrete.EntityFramework
{
    public class EFOrderDAL : EFRepositoryBase<Order, AppDbContext>, IOrderDAL
    {
        public void AddRange(int userId, List<Order> orders)
        {
            using var context = new AppDbContext();
            var result = orders.Select(x => { x.UserId = userId; x.CreatedDate = DateTime.Now; x.OrderNumber = Guid.NewGuid().ToString().Substring(0, 18); x.OrderEnum = OrderEnum.OnPending; return x; }).ToList();

            context.Orders.AddRange(result);
            context.SaveChanges();
        }

        public List<Order> GetOrderByUser(int userId)
        {
            try
            {
                using var context = new AppDbContext();

                // Kullanıcının ID'sine göre siparişlerini çekiyoruz.
                var userOrders = context.Orders
                    .Where(x => x.UserId == userId)
                    .Include(x => x.Product) // Bu satırı ekleyerek Product ilişkili verilerini çekiyoruz.
                    .ToList();

                return userOrders;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
