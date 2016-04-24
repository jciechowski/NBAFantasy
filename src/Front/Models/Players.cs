using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Front.Models
{
    public class Players
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Position { get; set; }
        public string Number { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string DOB { get; set; }
        public string Team { get; set; }
        public string Name { get; set; }
    }
}