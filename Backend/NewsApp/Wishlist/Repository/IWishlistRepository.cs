using System;
using Wishlist.Models;

namespace Wishlist.Repository
{
    public interface IWishlistRepository
    {
        Wish AddToWishlist(Wish wish );
        bool DeleteFromWishlist(string author,string userId);
        Wish SearchWishlist (string id ,string userId);
        List<Wish> GetAllWishlists(string userId);
        
    }
}
