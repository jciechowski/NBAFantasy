using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using NBAFantasy.Models;

namespace NBAFantasy.Controllers
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
            var allPlayers = _playerRepository.GetAllPlayers();
            return View(allPlayers);
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
        public IActionResult Edit(Player player, string id)
        {
            _playerRepository.Update(player, id);
            return View(player);
        }

        public IActionResult Delete(string id)
        {
            _playerRepository.Remove(id);
            return RedirectToAction(("Index"));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            if (player == null)
                return HttpNotFound();
            _playerRepository.Create(player);
            return View();
        }
    }
}