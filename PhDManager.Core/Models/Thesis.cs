using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PhDManager.Core.Models
{
    public class Thesis
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("year")]
        public int Year { get; set; }
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;
        [BsonElement("student")]
        public string Student { get; set; } = string.Empty;
        [BsonElement("supervisor")]
        public string Supervisor { get; set; } = string.Empty;
        [BsonElement("opponent")]
        public string Opponent { get; set; } = string.Empty;
    }
}
