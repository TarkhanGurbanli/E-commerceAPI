﻿using EcommerceApi.Business.Abstract;
using EcommerceApi.Entities.DTOs.WishListDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace EcommerceApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _wishListService;

        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }
        //[Authorize]
        [HttpPost("addwishlist/{productId}")]
        public IActionResult AddWishList([FromBody] WishListAddItemDTO wishListAddItemDTO)
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var userId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid").Value;
            var user = Convert.ToInt32(userId);

            var result = _wishListService.AddWishList(user, wishListAddItemDTO);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
        [HttpGet("userwishlist")]
        public IActionResult GetUserWishList()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(_bearer_token);
            var userId = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "nameid").Value;
            var user = Convert.ToInt32(userId);
            var result = _wishListService.GetUserWishList(user);

            if (result.Success) return Ok(result);

            return BadRequest(result);
        }
    }
}
