using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace NBAFantasy.Models
{
    public interface ITeamRepository
    {
        IEnumerable<Team> GetAllTeams();
        object FindById(string id);
        void Update(Team player, string id);
    }
    public class TeamRepository : ITeamRepository
    {
        private IMongoCollection<BsonDocument> _collection;
        private IMongoDatabase Database { get; set; }
        private IConfiguration Options { get; }

        public TeamRepository(IConfiguration dbConfiguration)
        {
            Options = dbConfiguration.GetSection("DatabaseSettings");
            Connect();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var documents = _collection.Find(new BsonDocument()).ToList();
            return documents.Select(bsonDoc => BsonSerializer.Deserialize<Team>(bsonDoc)).ToArray();
        }

        private void Connect()
        {
            var client = new MongoClient();
            var dbName = Options.GetSection("database").Value;
            var collection = Options.GetSection("teamsCollection").Value;
            Database = client.GetDatabase(dbName);
            _collection = Database.GetCollection<BsonDocument>(collection);
        }

        public object FindById(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var document = _collection.Find(filter).FirstOrDefault();
            return BsonSerializer.Deserialize<Team>(document);
        }

        public void Update(Team team, string id)
        {
            team.Id = ObjectId.Parse(id);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", team.Id);
            _collection.ReplaceOne(filter, BsonDocument.Parse(team.ToJson()));
        }
    }
}