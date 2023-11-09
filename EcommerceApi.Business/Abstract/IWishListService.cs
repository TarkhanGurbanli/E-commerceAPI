using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Entities.DTOs.WishListDTOs;

namespace EcommerceApi.Business.Abstract
{
    public interface IWishListService
    {
        IResult AddWishList(int userId, WishListAddItemDTO wishListAddItemDTO);
        IDataResult<List<WishListItemDTO>> GetUserWishList(int userId);
    }
}
