using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using FluentAssertions;

using ProxetTournamentFinale.Models;
using ProxetTournamentFinale.DataTransferObjects;
using ProxetTournamentFinale.Controllers;
using ProxetTournamentFinale.Tests.Mocks;

namespace ProxetTournamentFinale.Tests.Tests
{
    public class LobbyControllerTests
    {
        [Fact]
        public async Task Post_ShouldPostPlayer()
        {
            // Arrange
            var context = new PlayersContextMock();
            var controller = new LobbyController(context);
            var playerDto = new PlayerDto()
            {
                UserName = "abcd",
                VehicleType = 1
            };
            var player = new Player(playerDto);

            // Act
            var result = await controller.Post(playerDto);

            // Assert
            result.Should().BeOfType<OkResult>(
                "Post() should add player successfully"
            );

            var addedPlayer = context.Players.First();

            addedPlayer.UserName.Should().Be(
                playerDto.UserName,
                "Post() should add player with correct UserName"
            );

            addedPlayer.VehicleType.Should().Be(
                playerDto.VehicleType,
                "Post() should add player with correct VehicleType"
            );
        }
    }
}
