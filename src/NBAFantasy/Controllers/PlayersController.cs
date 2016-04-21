using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using ProductsAppTut.Models;


namespace ProductsAppTut.Controllers
{
    [Route("api/Players")]
    public class PlayersController : Controller
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayersController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<Players> GetAllProducts()
        {
            return _playerRepository.AllPlayers();
        }

        [HttpGet("{Name}", Name = "GetProduct")]
        public IActionResult GetProduct(string name)
        {
            var product = _playerRepository.GetByName(name);
            if (product == null)
                return HttpNotFound();
            return new ObjectResult(product);
        }

        [HttpPost]
        public void AddPlayer([FromBody] Players player)
        {
            if (player != null)
                _playerRepository.Add(player);
        }

    }
}
