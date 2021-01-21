using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ttt_service.Models
{
    public class GameModel
    {
        [Key]
        public Guid GameID { get; set; }

        public string BoardSpacesString { get; set; }

        [NotMapped]
        public int[] BoardSpaces 
        {
            get
            {
                return Array.ConvertAll(BoardSpacesString?.Split(';'), int.Parse);
            }
            set
            {
                BoardSpacesString = String.Join(';', value.Select(p => p.ToString()).ToArray());
            }
        }
        public int PlayerOneID { get; set; }
        public int PlayerTwoID { get; set; }
        public int WinnerID { get; set; }
    }
}
