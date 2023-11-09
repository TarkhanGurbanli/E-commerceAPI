using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Entities.DTOs.UserDTOs;

namespace EcommerceApi.Business.Abstract
{
    public interface IUserService
    {
        IResult Login(UserLoginDTO userLoginDTO);
        IResult Register(UserRegisterDTO userRegisterDTO);
        IResult VerifyEmail(string email, string ferifyToken);
        IDataResult<UserOrderDTO> GetUserOrders(int userId);
    }
}
