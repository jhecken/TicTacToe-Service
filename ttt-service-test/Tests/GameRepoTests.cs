using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ttt_service.Data;
using ttt_service.Models;
using Xunit;

namespace ttt_service_test
{
    public class GameRepoTests
    {
        [Fact]
        public async void CreateGameAddsNewGameToDb()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GameContext>()
                .UseInMemoryDatabase(databaseName: "testGameDbCreate")
                .Options;

            int p1Id = 1, p2Id = -1;
            var newGuid = Guid.NewGuid();

            var gameModel = new GameModel
            {
                GameID = newGuid,
                PlayerOneID = p1Id,
                PlayerTwoID = p2Id,
                BoardSpaces = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                WinnerID = -1
            };

            // act
            using (var context = new GameContext(options))
            {
                var gameRepo = new GameRepo(context);
                var returnVal = await gameRepo.CreateGame(gameModel);
            }

            //assert
            using (var context = new GameContext(options))
            {
                var games = context.Games
                    .Where(g => g.GameID == newGuid);

                Assert.Collection<GameModel>(games,
                    game =>
                    {
                        Assert.Equal(p1Id, game.PlayerOneID);
                        Assert.Equal(p2Id, game.PlayerTwoID);
                        Assert.Equal(newGuid, game.GameID);
                    });
            }
        }
    }
}
