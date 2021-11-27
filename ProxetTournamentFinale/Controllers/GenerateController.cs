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

                var chosenPlayers = new List<Player>[]
                {
                    new List<Player>(),
                    new List<Player>(),
                    new List<Player>()
                };

                for (int i = 0; i < 3; i++)
                {
                    chosenPlayers[i] = await playersDescending
                        .Where(p => p.VehicleType == i + 1)
                        .Take(6)
                        .ToListAsync();

                    _context.Players.RemoveRange(chosenPlayers[i]);
                }

                await _context.SaveChangesAsync();

                var compiledTeams = new PlayerDto[][]
                {
                    new PlayerDto[9],
                    new PlayerDto[9]
                };

                foreach (var team in compiledTeams)
                {
                    int teamMemberIndex = 0;

                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            team[teamMemberIndex++] =
                                new PlayerDto(chosenPlayers[i][j]);

                            chosenPlayers[i].RemoveAt(j);
                        }
                }

                var response = new GenerateTeamsResponse(
                    compiledTeams[0],
                    compiledTeams[1]
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