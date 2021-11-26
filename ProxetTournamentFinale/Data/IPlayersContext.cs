using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ProxetTournamentFinale.Models;

namespace ProxetTournamentFinale.Data
{
    public interface IPlayersContext : IDisposable
    {
        public DbSet<Player> Players { get; set; }

        public Task<int> SaveChangesAsync();
    }
}