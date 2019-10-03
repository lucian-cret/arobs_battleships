using Newtonsoft.Json;

namespace Arobs_Battleships.ViewModels.Home
{
    public class GridConfigurationViewModel
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Battleships { get; set; }
        public int Destroyers { get; set; }
    }
}
