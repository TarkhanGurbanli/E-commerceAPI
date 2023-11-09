using AutoMapper;
using EcommerceApi.Business.Abstract;
using EcommerceApi.Core.Utilities.Business;
using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Core.Utilities.Result.Concrete.ErrorResult;
using EcommerceApi.Core.Utilities.Result.Concrete.SuccessResult;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;
using EcommerceApi.Entities.Concrete.Enums;
using EcommerceApi.Entities.DTOs.OrderDTOs;
using EcommerceApi.Entities.DTOs.ProductDTOs;
using EcommerceApi.Entities.DTOs.UserDTOs;
using Microsoft.Extensions.Logging;

namespace EcommerceApi.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDAL _orderDAL;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly ILogger<OrderManager> _logger;

        public OrderManager(IUserService userService, IProductService productService, IMapper mapper, IOrderDAL orderDAL, ILogger<OrderManager> logger)
        {
            _userService = userService;
            _productService = productService;
            _mapper = mapper;
            _orderDAL = orderDAL;
            _logger = logger;
        }

        public IResult ChangeOrderStatus(string orderNumber, OrderEnum orderEnum)
        {
            try
            {
                var order = _orderDAL.Get(x => x.OrderNumber == orderNumber);

                if (order == null)
                    return new ErrorResult("Order not found");

                order.OrderEnum = orderEnum;
                _orderDAL.Update(order);
                return new SuccessResult("Order status changed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while changing order status: {ex.Message}");
                return new ErrorResult($"An error occurred while changing order status: {ex.Message}");
            }
        }

        public IResult CreateOrder(int userId, List<OrderCreateDTO> orderCreateDTOs)
        {
            try
            {
                var productIds = orderCreateDTOs.Select(x => x.ProductId).ToList();
                var quantities = orderCreateDTOs.Select(x => x.Quantity).ToList();
                var result = BusinessRules.Check(IsProductInStock(productIds));

                if (!result.Success)
                    return new ErrorResult("Not enough stock for the selected products");

                var map = _mapper.Map<List<Order>>(orderCreateDTOs);
                _orderDAL.AddRange(userId, map);
                var products = orderCreateDTOs.Select(x => new ProductDecrementQuantityDTO
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                }).ToList();
                _productService.RemoveProductCount(products);

                return new SuccessResult("Order created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while creating the order: {ex.Message}");
                return new ErrorResult($"An error occurred while creating the order: {ex.Message}");
            }
        }

        public IDataResult<UserOrderDTO> GetOrdersByUser(int userId)
        {
            try
            {
                // Kullanıcının ID'sine göre siparişleri çekiyoruz.
                var userOrders = _orderDAL.GetOrderByUser(userId);

                // Kullanıcının siparişleri yoksa ErrorDataResult ile hata döndürüyoruz.
                if (userOrders == null || !userOrders.Any())
                    return new ErrorDataResult<UserOrderDTO>("User orders not found");

                // Siparişleri UserOrderDTO'ya dönüştürüyoruz.
                var userOrderDTO = _mapper.Map<UserOrderDTO>(new User { Id = userId, Orders = userOrders });

                // Başarılı bir şekilde UserOrderDTO'yu dönüştürülmüş halde SuccessDataResult ile döndürüyoruz.
                return new SuccessDataResult<UserOrderDTO>(userOrderDTO, "User orders retrieved successfully");
            }
            catch (Exception ex)
            {
                // Hata durumunda teferruatlı loglama yapılır ve ErrorDataResult ile hata döndürülür.
                _logger.LogError($"An error occurred while getting orders by user: {ex.Message}");
                return new ErrorDataResult<UserOrderDTO>($"An error occurred while getting orders by user: {ex.Message}");
            }
        }



        private IResult IsProductInStock(List<int> productIds)
        {
            try
            {
                var product = _productService.CheckProductCount(productIds);

                if (!product.Data)
                    return new ErrorResult("Not enough stock for the selected products");

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while checking product stock: {ex.Message}");
                return new ErrorResult($"An error occurred while checking product stock: {ex.Message}");
            }
        }
    }
}
