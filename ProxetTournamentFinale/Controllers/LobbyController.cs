using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProxetTournamentFinale.Data;
using ProxetTournamentFinale.Models;
using ProxetTournamentFinale.DataTransferObjects;

namespace ProxetTournamentFinale.Controllers
{
    [Route("api/v1/[controller]")]
    public class LobbyController : ControllerBase
    {
        private readonly IPlayersContext _context;

        public LobbyController(IPlayersContext context)
        {
            _context = context;
        }

        // POST: api/v1/lobby
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PlayerDto player)
        {
            try
            {
                if (player == null)
                    return BadRequest();

                _context.Players.Add(new Player(player));
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}