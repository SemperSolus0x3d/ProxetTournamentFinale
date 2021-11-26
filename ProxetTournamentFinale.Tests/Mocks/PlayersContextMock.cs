using System;
using Microsoft.EntityFrameworkCore;

using ProxetTournamentFinale.Models;
using ProxetTournamentFinale.Data;
using System.Threading.Tasks;

namespace ProxetTournamentFinale.Tests.Mocks
{
    class PlayersContextMock : DbContext, IPlayersContext
    {

        public PlayersContextMock() :
            base(
                new DbContextOptionsBuilder<PlayersContextMock>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options
            )
        {
        }

        public DbSet<Player> Players { get; set; }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}
