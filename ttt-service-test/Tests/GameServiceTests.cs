using Moq;
using System;
using ttt_service.Data;
using ttt_service.Models;
using ttt_service.Services;
using ttt_service.Utils;
using Xunit;

namespace ttt_service_test
{
    public class GameServiceTests
    {
        private readonly GameService _gameService;
        private readonly Mock<IGameRepo> _mockGameRepo;
        private readonly Mock<IGameFactory> _mockGameFactory;

        public GameServiceTests()
        {
            _mockGameRepo = new Mock<IGameRepo>();
            _mockGameFactory = new Mock<IGameFactory>();
            _gameService = new GameService(_mockGameRepo.Object, _mockGameFactory.Object);
        }

        [Fact]
        public async void NewGameReturnsGameModel()
        {
            // arrange
            var playerOneId = 1;
            var playerTwoId = -1;
            var gameModel = new GameModel
            {
                PlayerOneID = playerOneId,
                PlayerTwoID = playerTwoId
            };

            _mockGameFactory.Setup(m => m.CreateGameInstance(playerOneId, playerTwoId)).Returns(gameModel);
            _mockGameRepo.Setup(m => m.CreateGame(gameModel)).ReturnsAsync(gameModel);

            // act
            var returnVal = await _gameService.NewGame(playerOneId, playerTwoId);

            // assert
            Assert.IsType<GameModel>(returnVal);
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

            _mockGameFactory.Setup(m => m.CreateGameInstance(playerOneId, playerTwoId)).Returns(gameModel);
            _mockGameRepo.Setup(m => m.CreateGame(gameModel)).ReturnsAsync(gameModel);

            // act
            var returnVal = await _gameService.NewGame(playerOneId, playerTwoId);

            // assert
            _mockGameRepo.Verify(m => m.CreateGame(gameModel), Times.Once());
        }
    }
}
