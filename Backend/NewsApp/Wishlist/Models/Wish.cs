using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wishlist.Models
{
    public class Wish
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? UrlToImage { get; set; }
        public string? Url { get; set; }

        public string? UserName { get; set; }
    }
}
