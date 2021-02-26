using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttt_service.Models;

namespace ttt_service.Services
{
    public interface IGameService
    {
        Task<GameUiModel> NewGame(int p1Id, int p2Id);

        Task<GameUiModel> GetGame(Guid id);

        Task<GameUiModel> MakeMove(Guid gameId, int spaceIndex, int playerNum);

        Task<GameUiModel> DeleteGame(Guid id);
    }
}
