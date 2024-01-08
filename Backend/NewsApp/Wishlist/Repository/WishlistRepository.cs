using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Wishlist.Models;

namespace Wishlist.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly WishlistContext _context;

        public WishlistRepository(WishlistContext context)
        {
            _context = context;
        }

        

        public Wish AddToWishlist(Wish wish )
        {
            _context.wishlists.InsertOne(wish);
            return wish;
        }

        public bool DeleteFromWishlist(string author, string userId)
        {
            var result = _context.wishlists.DeleteOne(w => w.Author == author);
            if (result.DeletedCount > 0)
            {
                return true; 
            }
            return false;
            
        }

        public List<Wish> GetAllWishlists(string userId)
        {
            return _context.wishlists.Find(w =>w.UserName == userId)
                .Project<Wish>(Builders<Wish>.Projection.Exclude("_"))
                .ToList();
        }

        public Wish SearchWishlist(string id, string userId)
        {
             return _context.wishlists.Find(w => w.Id == id && w.UserName == userId )
                .Project<Wish>(Builders<Wish>.Projection.Exclude("_"))
                .FirstOrDefault();
          
        }
    }
}
