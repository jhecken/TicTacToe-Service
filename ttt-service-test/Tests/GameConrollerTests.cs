using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using ttt_service.Controllers;
using ttt_service.Models;
using ttt_service.Services;
using Xunit;

namespace ttt_service_test
{
    public class GameConrollerTests
    {
        private readonly GameController _gameController;
        private readonly Mock<IGameService> _mockGameService;

        public GameConrollerTests()
        {
            _mockGameService = new Mock<IGameService>();
            _gameController = new GameController(_mockGameService.Object);
        }

        [Fact]
        public async void NewGameReturnsANewGameWithplayers()
        {            
            //arrange
            var gameGuid = Guid.NewGuid();
            int p1Id = 1, p2Id = -1;

            var gameModel = new GameModel
            {
                GameID = gameGuid,
                PlayerOneID = p1Id,
                PlayerTwoID = p2Id,
                BoardSpaces = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                WinnerID = -1
            };
            _mockGameService.Setup(m => m.NewGame(p1Id, p2Id))
                .ReturnsAsync(gameModel);

            //act
            //assert
            var returnVal = Assert.IsType<OkObjectResult>(await _gameController.NewGame(p1Id, p2Id));
            var returnGame = Assert.IsType<GameModel>(returnVal.Value);

            Assert.Equal(p1Id, returnGame.PlayerOneID);
            Assert.Equal(p2Id, returnGame.PlayerTwoID);

            _mockGameService.Verify(m => m.NewGame(p1Id, p2Id), Times.Once());
        }
        [Fact]
        public async void GetWithIdReturnsCorrectGame()
        {
            //arrange
            var gameGuid = Guid.NewGuid();
            int p1Id = 1, p2Id = -1;

            var gameModel = new GameModel
            {
                GameID = gameGuid,
                PlayerOneID = p1Id,
                PlayerTwoID = p2Id,
                BoardSpaces = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                WinnerID = -1
            };
            _mockGameService.Setup(m => m.GetGame(gameGuid))
                .ReturnsAsync(gameModel);

            //act
            //assert
            var returnVal = Assert.IsType<OkObjectResult>(await _gameController.Get(gameGuid));
            var returnGame = Assert.IsType<GameModel>(returnVal.Value);

            Assert.Equal(gameGuid, returnGame.GameID);
            Assert.Equal(p1Id, returnGame.PlayerOneID);
            Assert.Equal(p2Id, returnGame.PlayerTwoID);

            _mockGameService.Verify(m => m.GetGame(gameGuid), Times.Once());
        }

        [Fact]
        public async void PutReturnsCorrectGame()
        {
            //arrange
            var gameGuid = Guid.NewGuid();
            int p1Id = 1, p2Id = -1;

            var gameModel = new GameModel
            {
                GameID = gameGuid,
                PlayerOneID = p1Id,
                PlayerTwoID = p2Id,
                BoardSpaces = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 },
                WinnerID = -1
            };
            _mockGameService.Setup(m => m.MakeMove(gameGuid, 0, 1))
                .ReturnsAsync(gameModel);

            //act
            //assert
            var returnVal = Assert.IsType<OkObjectResult>(await _gameController.Put(gameGuid, 0, 1));
            var returnGame = Assert.IsType<GameModel>(returnVal.Value);

            Assert.Equal(gameGuid, returnGame.GameID);
            Assert.Equal(p1Id, returnGame.PlayerOneID);
            Assert.Equal(p2Id, returnGame.PlayerTwoID);

            _mockGameService.Verify(m => m.MakeMove(gameGuid, 0, 1), Times.Once());
        }
    }
}
