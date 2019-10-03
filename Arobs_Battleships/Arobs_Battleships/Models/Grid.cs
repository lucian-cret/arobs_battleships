using System;
using System.Collections.Generic;
using System.Linq;

namespace Arobs_Battleships.Models
{
    public class Grid
    {
        //private readonly int[] Rows = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        //private readonly char[] Columns = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }
        public IDictionary<string, Cell> Cells;
        public List<Ship> Ships { get; set; }

        public Grid(int numberOfRows, int numberOfColumns)
        {
            if (numberOfColumns > 26)
            {
                throw new ArgumentException("There are no more than 26 letters in the alphabet.");
            }
            Ships = new List<Ship>();
            Cells = new Dictionary<string, Cell>();
            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
            for (int i = 1; i <= NumberOfRows; i++)
            {
                for (int j = 65; j <= 65 + NumberOfColumns; j++)
                {
                    Cells.Add(((char)j).ToString() + i, new Cell(i, (char)j));
                }
            }
        }

        public Cell GetCellByCoordinates(char column, int row)
        {
            if (Cells.TryGetValue(column.ToString() + row, out Cell cell))
            {
                return cell;
            }
            return null;
        }

        public void BuildShip(int length)
        {
            if (length < 4)
            {
                throw new ArgumentException("Ships must be at least 4 squares long");
            }

            Ship ship = SetShipConfiguration(length);
            while (!IsShipValid(ship))
            {
                ship = SetShipConfiguration(length);
            }
        }

        public Ship GetCellShip(Cell cell)
        {
            foreach (var ship in Ships)
            {
                if (ship.Cells.Any(c => c.Row == cell.Row && c.Column == cell.Column))
                    return ship;
            }
            return null;
        }

        public ShotResponse ShotAtCell(char column, int row)
        {
            var cell = GetCellByCoordinates(column, row);
            switch (cell.State)
            {
                case CellState.IsWater:
                    cell.State = CellState.IsWaterHit;
                    break;
                case CellState.IsShip:
                    cell.State = CellState.IsShipHit;
                    break;
            }
            var ship = GetCellShip(cell);
            if (ship != null && ship.IsSunk)
            {
                ship.Cells.ForEach(c => c.State = CellState.IsShipSunk);
                if (Ships.TrueForAll(s => s.IsSunk))
                {
                    return new ShotResponse(ship.Cells, true);
                }
                return new ShotResponse(ship.Cells, false);
            }
            return new ShotResponse(new List<Cell> { cell }, false);
        }

        private Ship SetShipConfiguration(int length)
        {
            Random r = new Random();
            int row = r.Next(1, 11);
            char column = (char)r.Next(65, 75);
            Cell bow = new Cell(row, column);
            return new Ship(bow, (ShipOrientation)r.Next(4), length);
        }

        private bool IsShipValid(Ship ship)
        {
            ship.Cells = new List<Cell>();

            //check if outside of grid or overlapping. If all ok, launch ship at sea.
            switch (ship.Orientation)
            {
                case ShipOrientation.VerticalUp:
                    if (ship.ShipBow.Row - ship.Length < 1)
                        return false;
                    for (int i = ship.ShipBow.Row; i > ship.ShipBow.Row - ship.Length; i--)
                    {
                        if (!IsCellWater(ship.ShipBow.Column, i, ship.Cells))
                        {
                            return false;
                        }
                    }
                    break;
                case ShipOrientation.HorizontalRight:
                    if (ship.ShipBow.Column + ship.Length > 'J')
                        return false;
                    for (int i = ship.ShipBow.Column; i < ship.ShipBow.Column + ship.Length; i++)
                    {
                        if (!IsCellWater((char)i, ship.ShipBow.Row, ship.Cells))
                        {
                            return false;
                        }
                    }
                    break;
                case ShipOrientation.VerticalDown:
                    if (ship.ShipBow.Row + ship.Length > 10)
                        return false;
                    for (int i = ship.ShipBow.Row; i < ship.ShipBow.Row + ship.Length; i++)
                    {
                        if (!IsCellWater(ship.ShipBow.Column, i, ship.Cells))
                        {
                            return false;
                        }
                    }
                    break;
                case ShipOrientation.HorizontalLeft:
                    if (ship.ShipBow.Column - ship.Length < 'A')
                        return false;
                    for (int i = ship.ShipBow.Column; i > ship.ShipBow.Column - ship.Length; i--)
                    {
                        if (!IsCellWater((char)i, ship.ShipBow.Row, ship.Cells))
                        {
                            return false;
                        }
                    }
                    break;
            }
            ship.Cells.ForEach(c => c.State = CellState.IsShip);
            Ships.Add(ship);
            return true;
        }

        private bool IsCellWater(char column, int row, IList<Cell> cells)
        {
            var cell = GetCellByCoordinates(column, row);
            if (cell == null)
                throw new ArgumentException("Invalid cell coordinates.");
            cells.Add(cell);
            return cell.State == CellState.IsWater;
        }
    }
}
