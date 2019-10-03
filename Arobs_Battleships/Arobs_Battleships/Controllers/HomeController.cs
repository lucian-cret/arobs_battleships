using Arobs_Battleships.Models;
using Arobs_Battleships.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Arobs_Battleships.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static Grid grid;
        private readonly GridConfiguration _gridConfiguration;

        public HomeController(IOptions<GridConfiguration> gridConfiguration, ILogger<HomeController> logger)
        {
            _gridConfiguration = gridConfiguration.Value;
            _logger = logger;
        }
        public IActionResult Index()
        {
            try
            {
                grid = new Grid(_gridConfiguration.Rows, _gridConfiguration.Columns);
                //battleship
                grid.BuildShip(5);
                //destroyers
                grid.BuildShip(4);
                grid.BuildShip(4);

                var viewModel = new HomeViewModel(grid);
                return View(viewModel);
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
