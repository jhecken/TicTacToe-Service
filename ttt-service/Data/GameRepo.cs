using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttt_service.Models;

namespace ttt_service.Data
{
    public class GameRepo : IGameRepo
    {
        private readonly GameContext _gameContext;

        public GameRepo(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public async Task<GameModel> CreateGame(GameModel game)
        {
            var gameEntity = await _gameContext.AddAsync(game);
            await _gameContext.SaveChangesAsync();
            return gameEntity.Entity;

        }
    }
}
