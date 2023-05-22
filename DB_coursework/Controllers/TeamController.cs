using DB_coursework.Models;
using DB_coursework.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;

namespace DB_coursework.Controllers
{
    public class TeamController : Controller
    {
        private readonly ILogger<TeamController> _logger;

        private readonly ITeamRepository _teamRepository;
        public TeamController(ILogger<TeamController> logger, ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
            _logger = logger;
        }
        public IActionResult Edit(int id)
        {
            TeamModel team = _teamRepository.GetTeamById(id);
            return View("EditTeam", team);
        }

        [HttpPost]
        public IActionResult Edit(TeamModel team)
        {
            _teamRepository.UpdateTeam(team);
            return RedirectToAction("teams", "Team");
        }

        public IActionResult Create()
        {
            ViewBag.Teams = _teamRepository.GetAllTeams();
            return View("AddTeam");
        }

        [HttpPost]
        public IActionResult Create(TeamModel team)
        {
            _teamRepository.AddTeam(team);
            return RedirectToAction("teams", "Team");
        }

        public IActionResult Delete(int id)
        {
            _teamRepository.DeleteTeam(id);
            return RedirectToAction("teams", "Team");
        }

        public IActionResult Teams()
        {
            var teams = _teamRepository.GetAllTeams();
            return View(teams);
        }

        public IActionResult PopularRegion() 
        {
            var region = _teamRepository.GetMostPopularRegion();
            return View("RatedRegion", region);
        }
    }
}
