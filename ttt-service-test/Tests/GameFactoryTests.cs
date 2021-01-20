using System;
using System.Collections.Generic;
using System.Text;
using ttt_service.Models;
using ttt_service.Utils;
using Xunit;

namespace ttt_service_test.Tests
{
    public class GameFactoryTests
    {
        private readonly GameFactory _gameFactory;

        public GameFactoryTests()
        {
            _gameFactory = new GameFactory();
        }

        [Fact]
        public void NewGameReturnsGameModelWithCorrectPlayerIds()
        {
            // arrange
            var playerOneId = 1;
            var playerTwoId = -1;

            // act

            // assert
            var returnVal = Assert.IsType<GameModel>(_gameFactory.CreateGameInstance(playerOneId, playerTwoId));

            Assert.Equal(playerOneId, returnVal.PlayerOneID);
            Assert.Equal(playerTwoId, returnVal.PlayerTwoID);
        }

        [Fact]
        public void NewGameReturnsGameModelWithEmptyGameBoardAndNoWinner()
        {
            // arrange
            var playerOneId = 1;
            var playerTwoId = -1;

            // act

            // assert
            var returnVal = Assert.IsType<GameModel>(_gameFactory.CreateGameInstance(playerOneId, playerTwoId));
            Assert.Equal(9, returnVal.BoardSpaces.Length);
            Assert.All<int>(returnVal.BoardSpaces, space => Assert.Equal(-1, space));

            Assert.Equal(-1, returnVal.WinnerID);
        }
    }
}
