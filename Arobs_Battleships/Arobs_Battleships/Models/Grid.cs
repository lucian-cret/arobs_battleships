using System;
using System.Collections.Generic;
using System.Linq;

namespace Arobs_Battleships.Models
{
    public class Grid
    {
        private readonly int[] Rows = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private readonly char[] Columns = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        public IDictionary<string, Cell> Cells;
        public List<Ship> Ships { get; set; }

        public Grid()
        {
            Ships = new List<Ship>();
            Cells = new Dictionary<string, Cell>();
            foreach (var row in Rows)
            {
                foreach (var column in Columns)
                {
                    Cells.Add(column.ToString() + row, new Cell(row, column));
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
            if (length < 2)
            {
                throw new ArgumentException("Ships must be at least 2 squares long");
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
