﻿using System;
using System.Threading.Tasks;
using ttt_service.Data;
using ttt_service.Models;
using ttt_service.Utils;
using System.Linq;

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

        public async Task<GameUiModel> NewGame(int p1Id, int p2Id)
        {
            var newGame = _gameFactory.CreateGameInstance(p1Id, p2Id);

            newGame = await _gameRepo.CreateGame(newGame);
            var returnGame = new GameUiModel
            {
                GameID = newGame.GameID.ToString(),
                PlayerOneID = newGame.PlayerOneID,
                PlayerTwoID = newGame.PlayerTwoID,
                BoardSpaces = newGame.BoardSpaces,
                WinnerID = newGame.WinnerID
            };
            return returnGame;
        }

        public async Task<GameUiModel> GetGame(Guid id)
        {
            var game = await _gameRepo.GetGame(id);
            if (game == null)
                return null;

            var returnGame = new GameUiModel
            {
                GameID = game.GameID.ToString(),
                PlayerOneID = game.PlayerOneID,
                PlayerTwoID = game.PlayerTwoID,
                BoardSpaces = game.BoardSpaces,
                WinnerID = game.WinnerID
            };
            return returnGame;
        }

        public async Task<GameUiModel> MakeMove(Guid gameId, int spaceIndex, int playerNum)
        {
            if (playerNum != 1 && playerNum != 2)
                throw new ArgumentOutOfRangeException("Player number must be either a 1 or a 2.");
            if (spaceIndex < 0 || spaceIndex > 8)
                throw new ArgumentOutOfRangeException("Space Index must be between 0 and 8 (inclusive).");

            var game = await _gameRepo.GetGame(gameId);

            if (game.WinnerID != -1)
                throw new InvalidOperationException("The game has already been completed.");

            if (game.BoardSpaces[spaceIndex] != -1)
                throw new ArgumentOutOfRangeException("That space has already been claimed.");

            game.BoardSpaces[spaceIndex] = playerNum;

            CheckForEndOfGame(game);

            game = await _gameRepo.UpdateGame(game);
            var returnGame = new GameUiModel
            {
                GameID = game.GameID.ToString(),
                PlayerOneID = game.PlayerOneID,
                PlayerTwoID = game.PlayerTwoID,
                BoardSpaces = game.BoardSpaces,
                WinnerID = game.WinnerID
            };
            return returnGame;
        }

        public async Task<GameUiModel> DeleteGame(Guid id)
        {
            var game = await _gameRepo.DeleteGame(id);
            var returnGame = new GameUiModel
            {
                GameID = game.GameID.ToString(),
                PlayerOneID = game.PlayerOneID,
                PlayerTwoID = game.PlayerTwoID,
                BoardSpaces = game.BoardSpaces,
                WinnerID = game.WinnerID
            };
            return returnGame;
        }

        private void CheckForEndOfGame(GameModel game)
        {
            // Check for winning conditions
            if(game.BoardSpaces[0] != -1)
            {
                if(game.BoardSpaces[0] == game.BoardSpaces[1] && game.BoardSpaces[0] == game.BoardSpaces[2])
                {
                    game.WinnerID = game.BoardSpaces[0];
                    return;
                }

                if (game.BoardSpaces[0] == game.BoardSpaces[3] && game.BoardSpaces[0] == game.BoardSpaces[6])
                {
                    game.WinnerID = game.BoardSpaces[0];
                    return;
                }
                if (game.BoardSpaces[0] == game.BoardSpaces[4] && game.BoardSpaces[0] == game.BoardSpaces[8])
                {
                    game.WinnerID = game.BoardSpaces[0];
                    return;
                }
            }

            if (game.BoardSpaces[1] != -1)
            {
                if (game.BoardSpaces[1] == game.BoardSpaces[4] && game.BoardSpaces[1] == game.BoardSpaces[7])
                {
                    game.WinnerID = game.BoardSpaces[1];
                    return;
                }
            }

            if (game.BoardSpaces[2] != -1)
            {
                if (game.BoardSpaces[2] == game.BoardSpaces[5] && game.BoardSpaces[2] == game.BoardSpaces[8])
                {
                    game.WinnerID = game.BoardSpaces[2];
                    return;
                }
                if (game.BoardSpaces[2] == game.BoardSpaces[4] && game.BoardSpaces[2] == game.BoardSpaces[6])
                {
                    game.WinnerID = game.BoardSpaces[2];
                    return;
                }
            }

            if (game.BoardSpaces[3] != -1)
            {
                if (game.BoardSpaces[3] == game.BoardSpaces[4] && game.BoardSpaces[3] == game.BoardSpaces[5])
                {
                    game.WinnerID = game.BoardSpaces[3];
                    return;
                }
            }

            if (game.BoardSpaces[6] != -1)
            {
                if (game.BoardSpaces[6] == game.BoardSpaces[7] && game.BoardSpaces[6] == game.BoardSpaces[8])
                {
                    game.WinnerID = game.BoardSpaces[6];
                    return;
                }
            }

            // check for a draw
            if(game.BoardSpaces.Any(n => n == -1))
            {
                game.WinnerID = 0;
            }
        }
    }
}
