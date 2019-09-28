using Arobs_Battleships.Models;
using Arobs_Battleships.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace Arobs_Battleships.Controllers
{
    public class HomeController : Controller
    {
        private static Grid grid;
        public IActionResult Index()
        {
            grid = new Grid();
            //battleship
            grid.BuildShip(5);
            //destroyers
            grid.BuildShip(4);
            grid.BuildShip(4);

            var viewModel = new HomeViewModel(grid);
            return View(viewModel);
        }

        [Route("shoot")]
        public IActionResult Shoot(char column, int row)
        {
            var cell = grid.GetCellByCoordinates(column, row);
            switch (cell.State)
            {
                case CellState.IsWater:
                    cell.State = CellState.IsWaterHit;
                    break;
                case CellState.IsShip:
                    cell.State = CellState.IsShipHit;
                    break;
            }
            return Ok(Json(cell.State.ToString()));
        }
    }
}
