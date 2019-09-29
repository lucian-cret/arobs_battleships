using Arobs_Battleships.Models;
using Arobs_Battleships.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            var ship = grid.GetCellShip(cell);
            if (ship != null && ship.IsSunk)
            {
                ship.Cells.ForEach(c => c.State = CellState.IsShipSunk);
                if (grid.Ships.TrueForAll(s => s.IsSunk))
                {
                    return Ok(new ShotResponse(ship.Cells, true));
                }
                return Ok(new ShotResponse(ship.Cells, false));
            }
            return Ok(new ShotResponse(new List<Cell> { cell }, false));
        }
    }
}
