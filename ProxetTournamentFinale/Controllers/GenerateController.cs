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
using ProxetTournamentFinale.Responses;

namespace ProxetTournamentFinale.Controllers
{
    [Route("api/v1/teams/[controller]")]
    public class GenerateController : ControllerBase
    {
        private readonly IPlayersContext _context;

        public GenerateController(IPlayersContext context)
        {
            _context = context;
        }

        // POST: api/v1/teams/generate
        [HttpPost]
        public async Task<ActionResult<GenerateTeamsResponse>> Post()
        {
            try
            {
                // TODO: Implement matchmaking algorithm

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}