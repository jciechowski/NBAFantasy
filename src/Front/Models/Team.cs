﻿using System.Collections.Generic;
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
        [BsonIgnore]
        public IEnumerable<Player> Players { get; set; }
    }
}