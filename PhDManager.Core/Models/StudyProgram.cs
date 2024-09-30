using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PhDManager.Core.Models
{
    public class StudyProgram
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        [BsonElement("code")]
        public string Code { get; set; } = string.Empty;
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;
        [BsonElement("subjects")]
        public Subject[] Subjects { get; set; } = Array.Empty<Subject>();
    }
}
