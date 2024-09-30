using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PhDManager.Core.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;
        [BsonElement("displayName")]
        public string DisplayName { get; set; } = string.Empty;
        [BsonElement("firstName")]
        public string FirstName { get; set; } = string.Empty;
        [BsonElement("lastName")]
        public string LastName { get; set; } = string.Empty;
        [BsonElement("role")]
        public string Role { get; set; } = string.Empty;
        [BsonElement("firstLogin")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? FirstLogin { get; set; }
    }
}
