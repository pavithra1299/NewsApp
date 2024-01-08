using Wishlist.Models;

namespace Wishlist.Service
{
    public interface IWishlistService
    {
        Wish AddToWishlist(Wish wish ,string userName);
        bool DeleteFromWishlist(string author, string userId);
        Wish SearchWishlist(string id, string userId);
        List<Wish> GetAllWishLists(string userId);
    }
}
