using System.Collections.Generic;
using MongoDB.Bson;

namespace Front.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAllPlayers();

        Player FindById(string name);

        void Add(Player player);

        void Update(Player player, string id);

        bool Remove(ObjectId id);
    }
}