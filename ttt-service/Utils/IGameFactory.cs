﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttt_service.Models;

namespace ttt_service.Utils
{
    public interface IGameFactory
    {
        GameModel CreateGameInstance(int p1Id, int p2Id);
    }
}
