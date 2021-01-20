using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttt_service.Models;

namespace ttt_service.Data
{
    public class GameRepo : IGameRepo
    {
        public Task<GameModel> CreateGame(GameModel game)
        {
            throw new NotImplementedException();
        }
    }
}
