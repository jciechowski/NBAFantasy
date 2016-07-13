using Microsoft.AspNet.Mvc;
using NBAFantasy.Models;

namespace NBAFantasy.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IDbRepository _dbRepository;

        public PlayersController(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public IActionResult Index()
        {
            var players = _dbRepository.GetAllPlayers();
            foreach (var player in players)
            {
                player.Team = _dbRepository.FindTeamById(player.TeamId.ToString());
            }
            return View(players);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
                return HttpNotFound();

            var player = _dbRepository.FindPlayerById(id);
            if (player == null)
                return HttpNotFound();

            return View(player);
        }

        [HttpPost]
        public IActionResult Edit(Player player, string id)
        {
            _dbRepository.Update(player, id);
            return View(player);
        }

        public IActionResult Delete(string id)
        {
            _dbRepository.Remove(id);
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
            _dbRepository.Create(player);
            return View();
        }
    }
}