using Microsoft.EntityFrameworkCore;
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

        public async Task<GameModel> GetGame(Guid id)
        {
            return await _gameContext.Games.FindAsync(id);
        }

        public async Task<GameModel> UpdateGame(GameModel game)
        {
            var gameEntity = _gameContext.Update(game);
            await _gameContext.SaveChangesAsync();
            return gameEntity.Entity;
        }

        public async Task<GameModel> DeleteGame(Guid id)
        {
            var gameToDelete = await _gameContext.Games.FirstOrDefaultAsync<GameModel>(g => g.GameID == id);

            if(gameToDelete != null)
            {
                _gameContext.Remove<GameModel>(gameToDelete);
                await _gameContext.SaveChangesAsync();
            }
            return gameToDelete;
        }
    }
}
