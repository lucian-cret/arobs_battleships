using Arobs_Battleships.Models;

namespace Arobs_Battleships.ViewModels.Home
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
