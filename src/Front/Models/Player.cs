using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NBAFantasy.Models
{
    public class Player
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Position { get; set; }
        public string Number { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string DOB { get; set; }
        public ObjectId Team { get; set; }
        public string Name { get; set; }
    }
}