﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ttt_service.Services;
using ttt_service.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ttt_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/<GameController>
        [HttpGet]
        public async Task<IActionResult> NewGame(int playerOneId, int playerTwoId)
        {
            return Ok(await _gameService.NewGame(playerOneId, playerTwoId));
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _gameService.GetGame(id));
        }

        // POST api/<GameController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public Task<IActionResult> Put(Guid id, [FromBody] int spaceIndex, int player)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
