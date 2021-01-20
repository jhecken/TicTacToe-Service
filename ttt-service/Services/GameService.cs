using System;
using System.Threading.Tasks;
using ttt_service.Data;
using ttt_service.Models;
using ttt_service.Utils;

namespace ttt_service.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepo _gameRepo;
        private readonly IGameFactory _gameFactory;
        public GameService(IGameRepo gameRepo, IGameFactory gameFactory)
        {
            _gameRepo = gameRepo;
            _gameFactory = gameFactory;
        }

        public async Task<GameModel> NewGame(int p1Id, int p2Id)
        {
            var newGame = _gameFactory.CreateGameInstance(p1Id, p2Id);

            newGame = await _gameRepo.CreateGame(newGame);
            return newGame;
        }

        public async Task<GameModel> GetGame(Guid id)
        {
            return await _gameRepo.GetGame(id);
        }
    }
}
