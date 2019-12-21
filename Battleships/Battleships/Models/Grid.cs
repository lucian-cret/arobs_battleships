using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Models
{
    public class Grid
    {
        public int NumberOfRows { get; set; }
        public int NumberOfColumns { get; set; }
        public IDictionary<string, Cell> Cells;
        public List<Ship> Ships { get; set; }

        public Grid(int numberOfRows, int numberOfColumns)
        {
            if (numberOfRows < 5 || numberOfRows < 5)
            {
                throw new ArgumentException("Grid must be at least 5 x 5 squares");
            }
            Ships = new List<Ship>();
            Cells = new Dictionary<string, Cell>();
            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
            for (int i = 1; i <= NumberOfRows; i++)
            {
                for (int j = 65; j <= 64 + NumberOfColumns; j++)
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

            Ship ship = CreateShipConfiguration(length);
            while (!IsShipValid(ship))
            {
                ship = CreateShipConfiguration(length);
            }
            Ships.Add(ship);
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

        public ShotResponse ShootAtCell(char column, int row)
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

        private Ship CreateShipConfiguration(int length)
        {
            Random r = new Random();
            int row = r.Next(1, NumberOfRows + 1);
            char column = (char)r.Next(65, 64 + NumberOfColumns);
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
                    if (!ValidateVerticalUpShip(ship))
                    {
                        return false;
                    }
                    break;
                case ShipOrientation.HorizontalRight:
                    if (!ValidateHorizontalRightShip(ship))
                    {
                        return false;
                    }
                    break;
                case ShipOrientation.VerticalDown:
                    if (!ValidateVerticalDownShip(ship))
                    {
                        return false;
                    }
                    break;
                case ShipOrientation.HorizontalLeft:
                    if (!ValidateHorizontalLeftShip(ship))
                    {
                        return false;
                    }
                    break;
            }
            ship.Cells.ForEach(c => c.State = CellState.IsShip);
            return true;
        }
        private bool IsCellWater(Cell cell)
        {
            return cell != null && cell.State == CellState.IsWater;
        }

        private bool ValidateVerticalUpShip(Ship ship)
        {
            if (ship.ShipBow.Row - ship.Length < 1)
                return false;
            Cell currentCell = null;
            for (int i = ship.ShipBow.Row; i > ship.ShipBow.Row - ship.Length; i--)
            {
                currentCell = GetCellByCoordinates(ship.ShipBow.Column, i);
                if (!IsCellWater(currentCell))
                {
                    return false;
                }
                ship.Cells.Add(currentCell);
            }
            return true;
        }

        private bool ValidateHorizontalRightShip(Ship ship)
        {
            if (ship.ShipBow.Column + ship.Length > 64 + NumberOfColumns)
                return false;
            Cell currentCell = null;
            for (int i = ship.ShipBow.Column; i < ship.ShipBow.Column + ship.Length; i++)
            {
                currentCell = GetCellByCoordinates((char)i, ship.ShipBow.Row);
                if (!IsCellWater(currentCell))
                {
                    return false;
                }
                ship.Cells.Add(currentCell);
            }
            return true;
        }

        private bool ValidateVerticalDownShip(Ship ship)
        {
            if (ship.ShipBow.Row + ship.Length > NumberOfRows)
                return false;
            Cell currentCell = null;
            for (int i = ship.ShipBow.Row; i < ship.ShipBow.Row + ship.Length; i++)
            {
                currentCell = GetCellByCoordinates(ship.ShipBow.Column, i);
                if (!IsCellWater(currentCell))
                {
                    return false;
                }
                ship.Cells.Add(currentCell);
            }
            return true;
        }

        private bool ValidateHorizontalLeftShip(Ship ship)
        {
            if (ship.ShipBow.Column - ship.Length < 'A')
                return false;
            Cell currentCell = null;
            for (int i = ship.ShipBow.Column; i > ship.ShipBow.Column - ship.Length; i--)
            {
                currentCell = GetCellByCoordinates((char)i, ship.ShipBow.Row);
                if (!IsCellWater(currentCell))
                {
                    return false;
                }
                ship.Cells.Add(currentCell);
            }
            return true;
        }
    }
}
