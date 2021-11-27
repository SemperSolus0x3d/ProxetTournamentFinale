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
                var playersDescending =
                    _context.Players.OrderByDescending(p => p.EnqueueTime);

                var chosenPlayers = playersDescending
                    .GroupBy(p => p.VehicleType)
                    .ToList()
                    .Select(g => g.Take(6).ToList());

                var team1 = new List<Player>();
                var team2 = new List<Player>();

                foreach (var list in chosenPlayers)
                {
                    team1.AddRange(list.Take(3));
                    team2.AddRange(list.Skip(3));

                    _context.Players.RemoveRange(list);
                }

                await _context.SaveChangesAsync();

                var response = new GenerateTeamsResponse(
                    team1.Select(p => new PlayerDto(p)).ToArray(), 
                    team2.Select(p => new PlayerDto(p)).ToArray()
                );

                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}