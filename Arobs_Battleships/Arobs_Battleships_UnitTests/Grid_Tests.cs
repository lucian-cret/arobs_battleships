using Arobs_Battleships.Models;
using System;
using System.Linq;
using Xunit;

namespace Arobs_Battleships_UnitTests
{
    public class Grid_Tests
    {
        private readonly Grid grid;
        public Grid_Tests()
        {
            grid = new Grid(5, 5);
        }

        [Fact]
        public void Constructor_Success()
        {
            Assert.Equal(25, grid.Cells.Count);
            Assert.Equal("A1", grid.Cells.ElementAt(0).Key);
        }

        [Fact]
        public void Constructor_InvalidSize()
        {
            Assert.Throws<ArgumentException>(() => new Grid(3,3));
        }

        [Fact]
        public void GetCellByCoordinates_FirstCell()
        {
            var cell = grid.GetCellByCoordinates('A', 1);
            Assert.NotNull(cell);
        }

        [Fact]
        public void GetCellByCoordinates_InvalidCoordinates_ReturnsNull()
        {
            var cell = grid.GetCellByCoordinates('_', -10);
            Assert.Null(cell);
        }

        [Fact]
        public void BuildShip_InvalidLength_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => grid.BuildShip(2));
        }

        [Fact]
        public void BuildShip_Success()
        {
            grid.BuildShip(4);
            Assert.Single(grid.Ships);
            grid.Ships.Clear();
        }

    }
}
