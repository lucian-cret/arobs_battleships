namespace Battleships.Models
{
    public class Cell
    {
        public Cell(int row, char column)
        {
            Row = row;
            Column = column;
            State = CellState.IsWater;
        }
        public int Row { get; set; }
        public char Column { get; set; }
        public CellState State { get; set; }
    }
}
