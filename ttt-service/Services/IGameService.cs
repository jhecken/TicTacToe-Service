﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttt_service.Models;

namespace ttt_service.Services
{
    public interface IGameService
    {
        GameModel NewGame(int p1Id, int p2Id);
    }
}
