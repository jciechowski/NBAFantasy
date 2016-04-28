using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NBAFantasy.Models
{
    public class Team
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string GM { get; set; }
        // TODO
        // serializer żeby zamiast ObjectId wyciągać Player
        public IEnumerable<ObjectId> Players { get; set; }
    }
}