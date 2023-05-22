using DB_coursework.Models;
using DB_coursework.Repositories;
using DB_coursework.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DB_coursework.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ILogger<PlayerController> _logger;

        private readonly IPlayerRepository _playerRepository;

        private readonly ITeamRepository _teamRepository;
        public PlayerController(ILogger<PlayerController> logger, IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _logger = logger;
        }
        public IActionResult Edit(int id)
        {
            PlayerModel player = _playerRepository.GetPlayerById(id);
            return View("EditPlayer", player);
        }

        [HttpPost]
        public IActionResult Edit(PlayerModel player)
        {
            _playerRepository.UpdatePlayer(player);
            return RedirectToAction("players", "Player");
        }

        public IActionResult Create()
        {
            ViewBag.Teams = _teamRepository.GetAllTeams();
            return View("AddPlayer");
        }

        [HttpPost]
        public IActionResult Create(PlayerModel player)
        {
            System.Console.WriteLine($"{player.Team.Region}");
            _playerRepository.AddPlayer(player);
            return RedirectToAction("players", "Player");
        }

        public IActionResult Delete(int id)
        {
            _playerRepository.DeletePlayer(id);
            return RedirectToAction("players", "Player");
        }

        public IActionResult Players()
        {
            var players = _playerRepository.GetAllPlayers();
            return View(players);
        }

        public IActionResult PopularCountry()
        {
            var country = _playerRepository.GetMostPopularCountry();
            return View("RatedCountry", country);
        }
    }
}
