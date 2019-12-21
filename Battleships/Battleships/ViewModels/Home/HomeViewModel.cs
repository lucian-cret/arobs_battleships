using Battleships.Models;

namespace Battleships.ViewModels.Home
{
    public class HomeViewModel
    {
        public Grid Grid { get; set; }

        public HomeViewModel(Grid grid)
        {
            Grid = grid;
        }
    }
}
