using Microsoft.AspNet.Mvc;
using NBAFantasy.Models;

namespace NBAFantasy.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public IActionResult Index()
        {
            var allTeams = _teamRepository.GetAllTeams();
            return View(allTeams);
        }
    }
}