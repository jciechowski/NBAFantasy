using Microsoft.AspNetCore.Mvc;
using NBAFantasy.Models;

namespace NBAFantasy.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IDbRepository _dbRepository;

        public TeamsController(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        [Route("")]
        public IActionResult Host()
        {
            return View();
        }

        [Route("index")]
        public IActionResult Index()
        {
            var allTeams = _dbRepository.GetAllTeams();
            return PartialView(allTeams);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
                return NotFound();

            var team = _dbRepository.FindTeamById(id);
            if (team == null)
                return NotFound();

            return View(team);
        }

        [HttpPost]
        public IActionResult Edit(Team team, string id)
        {
            _dbRepository.Update(team, id);
            return View(team);
        }

        public IActionResult Details(string id)
        {
            TempData["TeamId"] = id;
            var team = _dbRepository.FindTeamById(id);
            team.Players = _dbRepository.FindPlayerByTeamId(id);
            return View(team);
        }

        public IActionResult Delete(string id)
        {
            _dbRepository.Delete(id);
            return RedirectToAction(("Index"));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            _dbRepository.Create(team);
            return RedirectToAction(("Index"));
        }

        public IActionResult AddPlayers()
        {
            var allPlayers = _dbRepository.GetAllPlayers();
            return View(allPlayers);
        }

        [HttpPost]
        public IActionResult AddPlayers(string[] selectedPlayers)
        {
//            var players = new List<Player>();
//            foreach (var selectedPlayer in selectedPlayers)
//                players.Add(_playerRepository.FindById(selectedPlayer));
//            var allPlayers = _dbRepository.GetAllPlayers();
//            _dbRepository.AddPlayer(selectedPlayers[0], TempData["TeamId"].ToString());
//            return View(allPlayers);
            return View();
        }
    }
}