using System;
using ttt_service.Models;
using ttt_service.Services;
using Xunit;

namespace ttt_service_test
{
    public class GameServiceTests
    {

        [Fact]
        public void NewGameReturnsGameModel()
        {
            // arrange
            GameService gameService = new GameService(null);
            var playerOneId = 1;
            var playerTwoId = -1;

            // act
            var returnVal = gameService.NewGame(playerOneId, playerTwoId);

            // assert
            Assert.IsType<GameModel>(returnVal);
        }

        [Fact]
        public void NewGameReturnsGameModelWithCorrectPlayerIds()
        {
            // arrange
            GameService gameService = new GameService(null);
            var playerOneId = 1;
            var playerTwoId = -1;

            // act

            // assert
            var returnVal = Assert.IsType<GameModel>(gameService.NewGame(playerOneId, playerTwoId));
            Assert.Equal(playerOneId, returnVal.PlayerOneID);
            Assert.Equal(playerTwoId, returnVal.PlayerTwoID);
        }

        [Fact]
        public void NewGameReturnsGameModelWithEmptyGameBoard()
        {
            // arrange
            GameService gameService = new GameService(null);
            var playerOneId = 1;
            var playerTwoId = -1;

            // act

            // assert
            var returnVal = Assert.IsType<GameModel>(gameService.NewGame(playerOneId, playerTwoId));
            Assert.Equal(9, returnVal.BoardSpaces.Length);
            Assert.All<int>(returnVal.BoardSpaces, space => Assert.Equal(-1, space));
        }
    }
}
