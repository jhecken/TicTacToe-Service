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

        public async Task<GameModel> MakeMove(Guid gameId, int spaceIndex, int playerNum)
        {
            if (playerNum != 1 && playerNum != 2)
                throw new ArgumentOutOfRangeException("Player number must be either a 1 or a 2.");
            if (spaceIndex < 0 || spaceIndex > 8)
                throw new ArgumentOutOfRangeException("Space Index must be between 0 and 8 (inclusive).");

            var game = await _gameRepo.GetGame(gameId);

            if (game.BoardSpaces[spaceIndex] != -1)
                throw new ArgumentOutOfRangeException("That space has already been claimed.");

            game.BoardSpaces[spaceIndex] = playerNum;

            return await _gameRepo.UpdateGame(game);
        }

        public async Task<GameModel> DeleteGame(Guid id)
        {
            return await _gameRepo.DeleteGame(id);
        }
    }
}
