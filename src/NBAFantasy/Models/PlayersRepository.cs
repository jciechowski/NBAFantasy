using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ProductsAppTut.Models
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

        public void Add(Players product)
        {
            var document = BsonDocument.Parse(product.ToJson());
            _collection.InsertOne(document);
        }

        public IEnumerable<Players> AllPlayers()
        {
            var documents = _collection.Find(new BsonDocument()).ToList();
            return documents.Select(bsonDocument => BsonSerializer.Deserialize<Players>(bsonDocument)).ToArray();
        }

        public Players GetByName(string name)
        {
            var filter = Builders<BsonDocument>.Filter.Regex("Name", new BsonRegularExpression(name));
            var document = _collection.Find(filter).First();
            return BsonSerializer.Deserialize<Players>(document);
        }

        public bool Remove(ObjectId id)
        {
            throw new NotImplementedException();
        }


        public void Update(Players product)
        {
            throw new NotImplementedException();
        }


    }
}
