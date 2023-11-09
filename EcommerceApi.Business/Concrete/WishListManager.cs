using AutoMapper;
using EcommerceApi.Business.Abstract;
using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Core.Utilities.Result.Concrete.ErrorResult;
using EcommerceApi.Core.Utilities.Result.Concrete.SuccessResult;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;
using EcommerceApi.Entities.DTOs.WishListDTOs;
using Microsoft.Extensions.Logging;

//Istifadecinin favorilerine istediyi mehsulu  elave edecek kod 

namespace EcommerceApi.Business.Concrete
{
    public class WishListManager : IWishListService
    {
        private readonly IWishListDAL _wishListDAL;
        private readonly IMapper _mapper;
        private readonly ILogger<WishListManager> _logger;


        public WishListManager(IMapper mapper, IWishListDAL wishListDAL, ILogger<WishListManager> logger)
        {
            _mapper = mapper;
            _wishListDAL = wishListDAL;
            _logger = logger;
        }

        public IResult AddWishList(int userId, WishListAddItemDTO wishListAddItemDTO)
        {
            var map = _mapper.Map<WishList>(wishListAddItemDTO);
            map.CreatedDate = DateTime.Now;
            map.UserId = userId;
            map.Status = true;
            _wishListDAL.Add(map);
            return new SuccessResult();
        }


        public IDataResult<List<WishListItemDTO>> GetUserWishList(int userId)
        {
            var userWishList = _wishListDAL.GetUserWishList(userId);

            if (!userWishList.Any())
                return new ErrorDataResult<List<WishListItemDTO>>();

            var map = _mapper.Map<List<WishListItemDTO>>(userWishList);

            return new SuccessDataResult<List<WishListItemDTO>>(map);
        }
    }
}
