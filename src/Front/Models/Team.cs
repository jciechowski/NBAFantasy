using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NBAFantasy.Models
{
    public class Team
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<Player> Players { get; set; }
    }
}