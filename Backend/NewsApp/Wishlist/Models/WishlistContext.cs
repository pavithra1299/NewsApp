using System;
using MongoDB.Driver;
namespace Wishlist.Models
{
    public class WishlistContext
    {
        private readonly IMongoDatabase _database = null;
        public WishlistContext(IConfiguration configuration)
        {
            //Initialize MongoClient and Database using connection string and database name from configuration
            // Get MongoDB connection string and database name from configuration
            var connectionString = configuration.GetSection("MongoDB:ConnectionString").Value.ToString();
            var databaseName = configuration.GetSection("MongoDB:WishlistDatabase").Value.ToString();

            // Initialize MongoClient and Database using connection string and database name
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        public IMongoCollection<Wish> wishlists
        {
            get
            {
                return _database.GetCollection<Wish>("wishlists");
            }
        }
    }
}
