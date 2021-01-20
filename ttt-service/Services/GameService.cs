using System;
using System.Threading.Tasks;
using ttt_service.Data;
using ttt_service.Models;

namespace ttt_service.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepo _gameRepo;
        public GameService(IGameRepo gameRepo)
        {
            _gameRepo = gameRepo;
        }
        public async Task<GameModel> NewGame(int p1Id, int p2Id)
        {
            var newGame = new GameModel
            {
                GameID = Guid.NewGuid(),
                PlayerOneID = p1Id,
                PlayerTwoID = p2Id,
                BoardSpaces = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                WinnerID = -1
            };

            await _gameRepo.CreateGame(null);
            return newGame;
        }
    }
}
