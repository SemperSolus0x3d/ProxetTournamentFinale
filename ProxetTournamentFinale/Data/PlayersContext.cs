using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProxetTournamentFinale.Models;

namespace ProxetTournamentFinale.Data
{
    public class PlayersContext : DbContext, IPlayersContext
    {
        public PlayersContext (DbContextOptions<PlayersContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}
