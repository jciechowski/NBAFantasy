using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace NBAFantasy.Models
{
    public interface IDbRepository
    {

        IEnumerable<Team> GetAllTeams();
        Team FindTeamById(string id);
        Player FindPlayerById(string id);
        void Update(Team team, string id);
        void Create(Team team);
        void Delete(string id);
        void AddPlayer(string playerId, string teamId);

        IEnumerable<Player> GetAllPlayers();

        void Create(Player player);

        void Update(Player player, string id);

        void Remove(string id);
    }

    public class DbRepository : IDbRepository
    {
        private IMongoCollection<BsonDocument> _playerCollection;
        private IMongoCollection<BsonDocument> _teamCollection;

        private IMongoDatabase Database { get; set; }
        private IConfiguration Options { get; }
        public DbRepository(IConfiguration dbConfiguration)
        {
            Options = dbConfiguration.GetSection("DatabaseSettings");
            Connect();
        }
        private void Connect()
        {
            var client = new MongoClient(Options.GetSection("connectionString").Value);
            Database = client.GetDatabase(Options.GetSection("database").Value);

            var playerCollection = Options.GetSection("playersCollection").Value;
            _playerCollection = Database.GetCollection<BsonDocument>(playerCollection);

            var teamCollection = Options.GetSection("teamsCollection").Value;
            _teamCollection = Database.GetCollection<BsonDocument>(teamCollection);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var documents = _teamCollection.Find(new BsonDocument()).ToList();
            return documents.Select(bsonDoc => BsonSerializer.Deserialize<Team>(bsonDoc)).ToList();
        }

        public Team FindTeamById(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var document = _teamCollection.Find(filter).FirstOrDefault();
            var team = BsonSerializer.Deserialize<Team>(document);
            return team;
        }

        public Player FindPlayerById(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var document = _playerCollection.Find(filter).FirstOrDefault();
            return BsonSerializer.Deserialize<Player>(document);
        }


        public void Update(Team team, string id)
        {

            team.Id = ObjectId.Parse(id);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", team.Id);
            _teamCollection.ReplaceOne(filter, BsonDocument.Parse(team.ToJson()));
        }

        public void Create(Team team)
        {
            var newTeam = BsonDocument.Parse(team.ToJson());
            _teamCollection.InsertOne(newTeam);
        }

        public void Delete(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            _teamCollection.DeleteOne(filter);
        }

        public void AddPlayer(string playerId, string teamId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            var documents = _playerCollection.Find(new BsonDocument()).ToList();
            return documents.Select(bsonDocument => BsonSerializer.Deserialize<Player>(bsonDocument)).ToArray();
        }

        public void Create(Player player)
        {
            var document = BsonDocument.Parse(player.ToJson());
            _playerCollection.InsertOne(document);
        }

        //TODO
        // remove ID and fix passing player ID from Edit view
        public void Update(Player player, string id)
        {
            player.Id = ObjectId.Parse(id);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", player.Id);
            _playerCollection.ReplaceOne(filter, BsonDocument.Parse(player.ToJson()));
        }

        public void Remove(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            _playerCollection.DeleteOne(filter);
        }
    }
}