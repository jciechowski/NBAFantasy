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

        //[HttpGet]
        public IEnumerable<Players> GetAllPlayers()
        {
            return _playerRepository.GetAllPlayers();
        }

        //[HttpGet("{Name}", Name = "GetPlayer")]
        public IActionResult GetPlayer()
        {
            var name = "Avery";
            var player = _playerRepository.GetByName(name);
            if (player == null)
                return HttpNotFound();
            ViewData["Player"] = player;
            return new ObjectResult(player);
        }

        //[HttpPost]
        public void AddPlayer([FromBody] Players player)
        {
            if (player != null)
                _playerRepository.Add(player);
        }

    }
}