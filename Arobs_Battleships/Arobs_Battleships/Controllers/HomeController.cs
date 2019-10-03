using Arobs_Battleships.Models;
using Arobs_Battleships.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Arobs_Battleships.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static Grid grid;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Post(GridConfigurationViewModel model)
        {
            try
            {
                grid = new Grid(model.Rows, model.Columns);
                for (int i = 0; i < model.Battleships; i++)
                {
                    grid.BuildShip(5);
                }
                for (int i = 0; i < model.Destroyers; i++)
                {
                    grid.BuildShip(4);
                }

                var viewModel = new HomeViewModel(grid);
                return View("Grid", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting new game");
                return View("Error");
            }
        }

        [Route("shoot")]
        public IActionResult Shoot(char column, int row)
        {
            return Ok(grid.ShotAtCell(column, row));
        }
    }
}
