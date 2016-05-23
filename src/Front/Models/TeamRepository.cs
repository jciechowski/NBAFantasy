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
        Team FindById(string id);
        void Update(Team player, string id);
        void Create(Team team);
        void Delete(string id);
        void AddPlayer(string playerId, string teamId);
    }
    public class TeamRepository : ITeamRepository
    {
        private IMongoCollection<BsonDocument> _collection;
        private IPlayerRepository _playerRepository;

        private IMongoDatabase Database { get; set; }
        private IConfiguration Options { get; }

        public TeamRepository(IConfiguration dbConfiguration, IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
            Options = dbConfiguration.GetSection("DatabaseSettings");
            Connect();
        }
        public IEnumerable<Team> GetAllTeams()
        {
            var documents = _collection.Find(new BsonDocument()).ToList();
            return documents.Select(bsonDoc => BsonSerializer.Deserialize<Team>(bsonDoc)).ToList();
        }

        private void Connect()
        {
            var client = new MongoClient();
            var dbName = Options.GetSection("database").Value;
            var collection = Options.GetSection("teamsCollection").Value;
            Database = client.GetDatabase(dbName);
            _collection = Database.GetCollection<BsonDocument>(collection);
        }

        public Team FindById(string id)
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

        public void Create(Team team)
        {
            var newTeam = BsonDocument.Parse(team.ToJson());
            _collection.InsertOne(newTeam);
        }

        public void Delete(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            _collection.DeleteOne(filter);
        }

        public void AddPlayer(string playerId, string teamId)
        {
            var player = _playerRepository.FindById(playerId);
            var team = FindById(teamId);
            var teamPlayers = team.Players;
            //_collection.InsertOne(player);
        }
    }
}