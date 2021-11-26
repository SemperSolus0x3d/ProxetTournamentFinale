using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FluentAssertions;
using Xunit;

using ProxetTournamentFinale.Models;
using ProxetTournamentFinale.Controllers;
using ProxetTournamentFinale.Tests.Mocks;

namespace ProxetTournamentFinale.Tests.Tests
{
    public class PlayersControllerTests
    {
        [Fact]
        public async Task GetPlayer_ShouldReturnAllPlayers()
        {
            var testPlayers = GetTestPlayers();
            var context = new PlayersContextMock();
            var controller = new PlayersController(context);

            await context.Players.AddRangeAsync(testPlayers);
            await context.SaveChangesAsync();
            var result = await controller.GetPlayer();

            result
                .Value.Intersect(testPlayers).Count()
                .Should().Be(
                    testPlayers.Length,
                    "GetPlayer() should return all players"
                );
        }

        [Fact]
        public async Task GetPlayer_ShouldReturnCorrectPlayer()
        {
            var testPlayers = GetTestPlayers();
            var context = new PlayersContextMock();
            var controller = new PlayersController(context);
            const int id = 3;

            await context.Players.AddRangeAsync(testPlayers);
            await context.SaveChangesAsync();
            var result = await controller.GetPlayer(id);

            result.Value.Id.Should().Be(
                id,
                "GetPlayer(int id) should return the correct player"
            );
        }

        [Fact]
        public async Task PostPlayer_ShouldPostPlayer()
        {
            var testPlayers = GetTestPlayers();
            var context = new PlayersContextMock();
            var controller = new PlayersController(context);
            var playerToPost = new Player() { Id = 10 };

            await context.Players.AddRangeAsync(testPlayers);
            await context.SaveChangesAsync();
            await controller.PostPlayer(playerToPost);
            var postedPlayer = (await controller.GetPlayer(playerToPost.Id)).Value;

            postedPlayer.Should().Be(
                playerToPost,
                "PostPlayer() should post the player"
            );
        }

        [Fact]
        public async Task GetPlayer_ShouldNotFindPlayer()
        {
            var testPlayers = GetTestPlayers();
            var context = new PlayersContextMock();
            var controller = new PlayersController(context);

            await context.Players.AddRangeAsync(testPlayers);
            await context.SaveChangesAsync();
            var result = await controller.GetPlayer(9999);

            result.Result.Should().BeOfType(
                typeof(NotFoundResult),
                "GetPlayer() should not find player"
            );
        }

        [Fact]
        public async Task DeletePlayer_ShouldDeletePlayer()
        {
            var testPlayers = GetTestPlayers();
            var context = new PlayersContextMock();
            var controller = new PlayersController(context);
            const int id = 3;

            await context.Players.AddRangeAsync(testPlayers);
            await context.SaveChangesAsync();
            var deletedPlayer = (await controller.DeletePlayer(id)).Value;
            var remainingPlayers = (await controller.GetPlayer()).Value;

            remainingPlayers.Should().NotContain(
                deletedPlayer,
                "DeletePlayer() should delete player"
            );
        }

        private Player[] GetTestPlayers() =>
            new Player[]
            {
                new Player() { Id = 1 },
                new Player() { Id = 2 },
                new Player() { Id = 3 },
                new Player() { Id = 4 },
                new Player() { Id = 5 }
            };
    }
}
