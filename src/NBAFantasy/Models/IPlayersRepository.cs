using MongoDB.Bson;
using System.Collections.Generic;

namespace ProductsAppTut.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<Players> AllPlayers();

        Players GetByName(string name);

        void Add(Players players);

        void Update(Players player);

        bool Remove(ObjectId id);
    }
}
