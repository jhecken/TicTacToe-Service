using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttt_service.Models;

namespace ttt_service.Data
{
    public interface IGameRepo
    {
        Task<GameModel> CreateGame(GameModel game);
    }
}
