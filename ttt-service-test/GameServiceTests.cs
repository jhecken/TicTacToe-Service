using Moq;
using System;
using ttt_service.Data;
using ttt_service.Models;
using ttt_service.Services;
using Xunit;

namespace ttt_service_test
{
    public class GameServiceTests
    {
        private readonly GameService _gameService;
        private readonly Mock<IGameRepo> _mockGameRepo;

        public GameServiceTests()
        {
            _mockGameRepo = new Mock<IGameRepo>();
            _gameService = new GameService(_mockGameRepo.Object);
        }

        [Fact]
        public async void NewGameReturnsGameModel()
        {
            // arrange
            var playerOneId = 1;
            var playerTwoId = -1;

            // act
            var returnVal = await _gameService.NewGame(playerOneId, playerTwoId);

            // assert
            Assert.IsType<GameModel>(returnVal);
        }

        [Fact]
        public async void NewGameReturnsGameModelWithCorrectPlayerIds()
        {
            // arrange
            var playerOneId = 1;
            var playerTwoId = -1;

            // act

            // assert
            var returnVal = Assert.IsType<GameModel>(await _gameService.NewGame(playerOneId, playerTwoId));
            Assert.Equal(playerOneId, returnVal.PlayerOneID);
            Assert.Equal(playerTwoId, returnVal.PlayerTwoID);
        }

        [Fact]
        public async void NewGameReturnsGameModelWithEmptyGameBoardAndNoWinner()
        {
            // arrange
            var playerOneId = 1;
            var playerTwoId = -1;

            // act

            // assert
            var returnVal = Assert.IsType<GameModel>(await _gameService.NewGame(playerOneId, playerTwoId));
            Assert.Equal(9, returnVal.BoardSpaces.Length);
            Assert.All<int>(returnVal.BoardSpaces, space => Assert.Equal(-1, space));

            Assert.Equal(-1, returnVal.WinnerID);
        }

        [Fact]
        public async void NewGameCallsRepoNewGame()
        {
            // arrange
            var playerOneId = 1;
            var playerTwoId = -1;
            var gameModel = new GameModel
            {
                PlayerOneID = playerOneId,
                PlayerTwoID = playerTwoId
            };

            _mockGameRepo.Setup(m => m.CreateGame(null)).ReturnsAsync(gameModel);

            // act
            var returnVal = await _gameService.NewGame(playerOneId, playerTwoId);

            // assert
            _mockGameRepo.Verify(m => m.CreateGame(null), Times.Once());
        }
    }
}
