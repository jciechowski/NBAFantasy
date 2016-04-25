using System.Collections.Generic;
using Front.Models;
using Microsoft.AspNet.Mvc;

namespace Front.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public IActionResult Index()
        {
            var allPlayers = GetAllPlayers();
            return View(allPlayers);
        }

        public IEnumerable<Players> GetAllPlayers()
        {
            return _playerRepository.GetAllPlayers();
        }

        public void AddPlayer([FromBody] Players player)
        {
            if (player != null)
                _playerRepository.Add(player);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
                return HttpNotFound();

            var player = _playerRepository.FindById(id);
            if (player == null)
                return HttpNotFound();

            return View(player);
        }

        [HttpPost]
        public IActionResult Edit(Players player, string id)
        {
            _playerRepository.Update(id);
            return View(player);
        }

    }
}