using System.Collections.Generic;
using MongoDB.Bson;

namespace Front.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<Players> GetAllPlayers();

        Players GetByName(string name);

        void Add(Players players);

        void Update(Players player);

        bool Remove(ObjectId id);
    }
}