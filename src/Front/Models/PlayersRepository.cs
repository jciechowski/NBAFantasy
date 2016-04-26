using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq;

namespace Front.Models
{
    public class PlayersRepository : IPlayerRepository
    {
        private IMongoCollection<BsonDocument> _collection;
        private IMongoDatabase Database { get; set; }
        private IConfiguration Options { get; }

        public PlayersRepository(IConfiguration configuration)
        {
            Options = configuration.GetSection("DatabaseSettings");
            Connect();
        }

        private void Connect()
        {
            var client = new MongoClient();
            var dbName = Options.GetSection("database").Value;
            var collection = Options.GetSection("collection").Value;
            Database = client.GetDatabase(dbName);
            _collection = Database.GetCollection<BsonDocument>(collection);
        }

        public void Add(Player player)
        {
            var document = BsonDocument.Parse(player.ToJson());
            _collection.InsertOne(document);
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            var documents = _collection.Find(new BsonDocument()).ToList();
            return documents.Select(bsonDocument => BsonSerializer.Deserialize<Player>(bsonDocument)).ToArray();
        }

        public Player FindById(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var document = _collection.Find(filter).FirstOrDefault();
            return BsonSerializer.Deserialize<Player>(document);
        }

        public bool Remove(ObjectId id)
        {
            throw new NotImplementedException();
        }
        //TODO
        // remove ID and fix passing player ID from Edit view
        public void Update(Player player, string id)
        {
            player.Id = ObjectId.Parse(id);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", player.Id);
            _collection.ReplaceOne(filter, BsonDocument.Parse(player.ToJson()));
        }

    }
}