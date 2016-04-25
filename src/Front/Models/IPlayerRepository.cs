using System.Collections.Generic;
using MongoDB.Bson;

namespace Front.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<Players> GetAllPlayers();

        Players FindById(string name);

        void Add(Players players);

        void Update(string id);

        bool Remove(ObjectId id);
    }
}