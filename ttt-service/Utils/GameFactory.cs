using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttt_service.Models;

namespace ttt_service.Utils
{
    public class GameFactory : IGameFactory
    {
        public GameModel CreateGameInstance(int p1Id, int p2Id)
        {
            return new GameModel
            {
                GameID = Guid.NewGuid(),
                PlayerOneID = p1Id,
                PlayerTwoID = p2Id,
                BoardSpaces = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                WinnerID = -1
            };
        }
    }
}
