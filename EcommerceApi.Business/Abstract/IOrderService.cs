using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Entities.Concrete.Enums;
using EcommerceApi.Entities.DTOs.OrderDTOs;
using EcommerceApi.Entities.DTOs.UserDTOs;

namespace EcommerceApi.Business.Abstract
{
    public interface IOrderService
    {
        IResult CreateOrder(int userId, List<OrderCreateDTO> orderCreateDTOs);
        IResult ChangeOrderStatus(string orderNumber, OrderEnum orderEnum);
        IDataResult<UserOrderDTO> GetOrdersByUser(int userId);
    }
}
