using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ttt_service.Models
{
    public class GameUiModel
    {
        public string GameID { get; set; }
        public int[] BoardSpaces { get; set; }
        public int PlayerOneID { get; set; }
        public int PlayerTwoID { get; set; }
        public int WinnerID { get; set; }
    }
}
