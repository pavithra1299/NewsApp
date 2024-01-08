using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Wishlist.Models;
using Wishlist.Repository;
using Wishlist.Service;
using Wishlist.Exceptions;

namespace Wishlist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly ILogger _logger;
        public WishlistController(IWishlistService wishlistService, ILogger<WishlistController> logger)
        {
            _wishlistService = wishlistService;
            _logger = logger;
            _logger.LogInformation(message: "Wishlist constructor is called");
        }

        [HttpPost]
        public IActionResult AddToWishList([FromBody] Wish wishList,string userName)
        {
            // You might want to validate the wishList here
            var addedWishList = _wishlistService.AddToWishlist(wishList,userName);
            _logger.LogInformation(message: "Data added to Wishlist");
            return CreatedAtAction(nameof(GetAllWishLists), addedWishList);
        }

        [HttpDelete("{author}")]
        public IActionResult DeleteFromWishlist(string author, string userName)
        {
            try
            {
                var isDeleted = _wishlistService.DeleteFromWishlist(author, userName);

                if (isDeleted)
                {
                    _logger.LogInformation(message: "Data deleted from Wishlist");
                    return Ok(true);
                }

                else
                {
                    _logger.LogInformation(message: "Username not found");
                    return NotFound("This  id not found");
                }
            }
            catch (InvalidIdException ex)
            {
                _logger.LogInformation(message: "Id not found");
                return NotFound(ex.Message);
            }

        }

        [HttpGet("{id}", Name = "GetWishListById")]
        public IActionResult GetWishList(string id, string userId)
        {
            var wishList = _wishlistService.SearchWishlist(id, userId);

            if (wishList == null)
            {
                return NotFound();
            }
            _logger.LogInformation(message: "Id not found");
            return Ok(wishList);
        }

        [HttpGet]
        public IActionResult GetAllWishLists(string userName)
        {
            var wishLists = _wishlistService.GetAllWishLists(userName);
            _logger.LogInformation(message: "Data fetched successfully!!");
            return Ok(wishLists);
        }
    }
}
