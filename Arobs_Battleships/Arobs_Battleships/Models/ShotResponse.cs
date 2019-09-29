using Newtonsoft.Json;
using System.Collections.Generic;

namespace Arobs_Battleships.Models
{
    public class ShotResponse
    {
        [JsonProperty("cells")]
        public List<Cell> CellData { get; set; }

        [JsonProperty("isgamewon")]
        public bool IsGameWon { get; set; }

        public ShotResponse(List<Cell> cellData, bool isGameWon)
        {
            CellData = cellData;
            IsGameWon = isGameWon;
        }
    }
}
