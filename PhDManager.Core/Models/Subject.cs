using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PhDManager.Core.Models
{
    public class Subject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("code")]
        public string Code { get; set; } = string.Empty;
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("credits")]
        public int Credits { get; set; }
        [BsonElement("semester")]
        public string Semester { get; set; } = string.Empty;
    }
}
