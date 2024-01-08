using System;
using System.Collections.Generic;
using System.Linq;
using Wishlist.Models;
using Wishlist.Repository;
using Wishlist.Exceptions;

namespace Wishlist.Service
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }
        public Wish AddToWishlist(Wish wish,string userName)
        {
            var wishList = new Wish
            {
               Title = wish.Title,
               Description = wish.Description,
               Author = wish.Author,
               Url = wish.Url,
               UrlToImage = wish.UrlToImage,
               UserName = userName
            };

            return _wishlistRepository.AddToWishlist(wishList);
        }

        public bool DeleteFromWishlist(string author, string userId)
        {
            var res = _wishlistRepository.DeleteFromWishlist(author, userId);

            if (!res)
            {
                throw new InvalidIdException("Id not found");
            }

            return true;

        }

        public List<Wish> GetAllWishLists(string userId)
        {
            return _wishlistRepository.GetAllWishlists(userId);
        }

        public Wish SearchWishlist(string id, string userId)
        {
            return _wishlistRepository.SearchWishlist(id, userId);
        }
    }
}
