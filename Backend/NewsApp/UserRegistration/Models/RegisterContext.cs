using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace UserRegistration.Models
{
    public class RegisterContext
    {
        private readonly IMongoDatabase _database;

        // Constructor that takes IConfiguration as a parameter to get connection string and database name
        public RegisterContext(IConfiguration configuration)
        {
            // Get MongoDB connection string and database name from configuration
            string connectionString = configuration.GetSection("MongoDB:ConnectionString").Value.ToString();
            string databaseName = configuration.GetSection("MongoDB:DatabaseName").Value.ToString();

            // Initialize MongoClient and Database using connection string and database name
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        // Define a MongoCollection to represent the Users collection of MongoDB
        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }
    }
}
