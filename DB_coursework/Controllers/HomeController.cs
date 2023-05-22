using DB_coursework.Models;
using DB_coursework.Repositories;
using DB_coursework.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DB_coursework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ITeamRepository _teamRepository;

        private readonly IPlayerRepository _playerRepository;
        public HomeController(ILogger<HomeController> logger, ITeamRepository teamRepository, IPlayerRepository playerRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
