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

        [Fact]
        public async void GetGameCallsRepoGetGame()
        {
            // arrange
            var playerOneId = 1;
            var playerTwoId = -1;
            var gameId = Guid.NewGuid();
            var gameModel = new GameModel
            {
                GameID = gameId,
                PlayerOneID = playerOneId,
                PlayerTwoID = playerTwoId
            };

            _mockGameRepo.Setup(m => m.GetGame(gameId)).ReturnsAsync(gameModel);

            // act
            var returnVal = await _gameService.GetGame(gameId);

            // assert
            _mockGameRepo.Verify(m => m.GetGame(gameId), Times.Once());
        }

        [Fact]
        public async void MakeMoveThowsExceptionWhenInvalidPlayer()
        {
            //arrange
            var playerNum = 5;
            var gameGuid = Guid.NewGuid();
            var spaceIndex = 4;

            //act
            //assert
            var result = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _gameService.MakeMove(gameGuid, spaceIndex, playerNum));
        }
        [Fact]
        public async void MakeMoveThowsExceptionWhenInvalidIndex()
        {
            //arrange
            var playerNum = 1;
            var gameGuid = Guid.NewGuid();
            var spaceIndex = 9;

            //act
            //assert
            var result = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _gameService.MakeMove(gameGuid, spaceIndex, playerNum));
        }

        [Fact]
        public async void MakeMoveThrowsWhenSpaceAlreadyClaimed()
        {
            // arrange
            var playerOneId = 1;
            var playerTwoId = -1;
            var gameId = Guid.NewGuid();

            var gameModel = new GameModel
            {
                GameID = gameId,
                PlayerOneID = playerOneId,
                PlayerTwoID = playerTwoId,
                BoardSpaces = new int[] { 1, -1, -1, -1, -1, -1, -1, -1, -1 },
                WinnerID = -1
            };

            _mockGameRepo.Setup(m => m.GetGame(gameId)).ReturnsAsync(gameModel);

            // act
            // assert
            var result = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _gameService.MakeMove(gameId, 0, 1));
        }

        [Fact]
        public async void MakeMoveCallsRepoGetGameAndUpdateGame()
        {
            // arrange
            var playerOneId = 1;
            var playerTwoId = -1;
            var gameId = Guid.NewGuid();
            var gameModel = new GameModel
            {
                GameID = gameId,
                PlayerOneID = playerOneId,
                PlayerTwoID = playerTwoId,
                BoardSpaces = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                WinnerID = -1
            };

            _mockGameRepo.Setup(m => m.GetGame(gameId)).ReturnsAsync(gameModel);
            _mockGameRepo.Setup(m => m.UpdateGame(gameModel)).ReturnsAsync(gameModel);

            // act
            var returnVal = await _gameService.MakeMove(gameId, 0, 1);

            // assert
            _mockGameRepo.Verify(m => m.GetGame(gameId), Times.Once());
            _mockGameRepo.Verify(m => m.UpdateGame(gameModel), Times.Once());

        }
    }
}
