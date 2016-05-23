using Microsoft.AspNet.Mvc;
using NBAFantasy.Models;
using System.Collections.Generic;

namespace NBAFantasy.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IPlayerRepository _playerRepository;

        public TeamsController(ITeamRepository teamRepository, IPlayerRepository playerRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
        }

        public IActionResult Index()
        {
            var allTeams = _teamRepository.GetAllTeams();
            return View(allTeams);
        }

        public IActionResult Edit(string id)
        {
            if (id == null)
                return HttpNotFound();

            var team = _teamRepository.FindById(id);
            if (team == null)
                return HttpNotFound();

            return View(team);
        }

        [HttpPost]
        public IActionResult Edit(Team team, string id)
        {
            _teamRepository.Update(team, id);
            return View(team);
        }

        public IActionResult Details(string id)
        {
            TempData["TeamId"] = id;
            var team = _teamRepository.FindById(id);
            return View(team);
        }

        public IActionResult Delete(string id)
        {
            _teamRepository.Delete(id);
            return RedirectToAction(("Index"));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            team.Players = new List<Player>();
            _teamRepository.Create(team);
            return RedirectToAction(("Index"));
        }

        public IActionResult AddPlayers()
        {
            var allPlayers = _playerRepository.GetAllPlayers();
            return View(allPlayers);
        }

        [HttpPost]
        public IActionResult AddPlayers(string[] selectedPlayers)
        {
            var players = new List<Player>();
            foreach (var selectedPlayer in selectedPlayers)
                players.Add(_playerRepository.FindById(selectedPlayer));
            var allPlayers = _playerRepository.GetAllPlayers();
            _teamRepository.AddPlayer(selectedPlayers[0], TempData["TeamId"].ToString());
            return View(allPlayers);
        }
    }
}