namespace Arobs_Battleships.Models
{
    public class Ship
    {
        public Cell ShipBow { get; set; }
        public ShipOrientation Orientation { get; set; }
        public int Length { get; set; }
        public Ship(Cell bow, ShipOrientation orientation, int length)
        {
            ShipBow = bow;
            Orientation = orientation;
            Length = length;
        }
    }
}
