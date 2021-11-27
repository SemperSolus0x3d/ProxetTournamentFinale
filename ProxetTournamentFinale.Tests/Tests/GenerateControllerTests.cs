using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using FluentAssertions;

using ProxetTournamentFinale.Models;
using ProxetTournamentFinale.DataTransferObjects;
using ProxetTournamentFinale.Controllers;
using ProxetTournamentFinale.Responses;
using ProxetTournamentFinale.Tests.Mocks;

namespace ProxetTournamentFinale.Tests.Tests
{
    public class GenerateControllerTests
    {
        private readonly string[] _class1Users =
        {
            "_{Basil$@",
            "o-Ben#-",
            "+oBarney_+",
            "#%Gerard@%",
            "-@Grant]%",
            "<OEmil%#",
        };

        private readonly string[] _class2Users =
        {
            "O$Mickey$@",
            "$-Dereko-",
            "o@Rusty#*",
            "^[Andres_$",
            "*<Tomas$_",
            "##Dwayne_o",
        };

        private readonly string[] _class3Users =
        {
            "O#Truman#@",
            "*%Wesley-+",
            "-{Sidney_o",
            "[#Andre]O",
            "-[Oliver}@",
            "^[Gene*+",
        };

        private readonly Player[] _input =
        {
            // Players of first vehicle type
            new Player() {
                UserName = "_{Basil$@",
                VehicleType = 1,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player() {
                UserName = "o-Ben#-",
                VehicleType = 1,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player() {
                UserName = "+oBarney_+",
                VehicleType = 1,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player() {
                UserName = "#%Gerard@%",
                VehicleType = 1,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player() {
                UserName = "-@Grant]%",
                VehicleType = 1,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player() {
                UserName = "<OEmil%#",
                VehicleType = 1,
                EnqueueTime = new DateTime(2000, 1, 1)
            },

            // Players of second vehicle type
            new Player()
            {
                UserName = "O$Mickey$@",
                VehicleType = 2,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player()
            {
                UserName = "$-Dereko-",
                VehicleType = 2,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player()
            {
                UserName = "o@Rusty#*",
                VehicleType = 2,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player()
            {
                UserName = "^[Andres_$",
                VehicleType = 2,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player()
            {
                UserName = "*<Tomas$_",
                VehicleType = 2,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player()
            {
                UserName = "##Dwayne_o",
                VehicleType = 2,
                EnqueueTime = new DateTime(2000, 1, 1)
            },

            // Players of third vehicle type
            new Player()
            {
                UserName = "O#Truman#@",
                VehicleType = 3,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player()
            {
                UserName = "*%Wesley-+",
                VehicleType = 3,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player()
            {
                UserName = "-{Sidney_o",
                VehicleType = 3,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player()
            {
                UserName = "[#Andre]O",
                VehicleType = 3,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player()
            {
                UserName = "-[Oliver}@",
                VehicleType = 3,
                EnqueueTime = new DateTime(2000, 1, 1)
            },
            new Player()
            {
                UserName = "^[Gene*+",
                VehicleType = 3,
                EnqueueTime = new DateTime(2000, 1, 1)
            },

            // Some random players
            new Player()
            {
                UserName = "vv7s86v68s",
                VehicleType = 1,
                EnqueueTime = new DateTime(2005, 1, 1)
            },
            new Player()
            {
                UserName = "svsvbnijbn",
                VehicleType = 2,
                EnqueueTime = new DateTime(2005, 1, 1)
            },
            new Player()
            {
                UserName = "v5cw4wxxkl",
                VehicleType = 3,
                EnqueueTime = new DateTime(2005, 2, 1)
            },
            new Player()
            {
                UserName = "v5cwbjkbn",
                VehicleType = 1,
                EnqueueTime = new DateTime(2005, 2, 5)
            }
        };
    
        [Fact]
        public async Task Post_ShouldReturnIdealTeam()
        {
            var context = new PlayersContextMock();
            var controller = new GenerateController(context);
            var input = GetTestInput();

            foreach (var player in input)
                await context.Players.AddAsync(player);

            await context.SaveChangesAsync();

            var result = await controller.Post();

            result.Result.Should().BeOfType<OkResult>(
                "Post() should compile teams successfully"
            );

            AssertClasses(result.Value);

            result.Value.FirstTeam.Select(p => p.UserName)
                .Intersect(
                    result.Value.SecondTeam.Select(p => p.UserName)
                )
                .Count()
                .Should()
                .Be(0, "Every player should appear only in one team");
        }

        private List<Player> GetTestInput()
        {
            var result = _input.ToList();
            Shuffle(result);
            return result;
        }

        private void Shuffle<T>(List<T> list)
        {
            Random rand = new Random();

            for (int i = list.Count - 1; i > 1; i--)
            {
                int k = rand.Next(i + 1);

                T temp = list[k];
                list[k] = list[i];
                list[i] = temp;
            }
        }

        private void AssertClasses(GenerateTeamsResponse response)
        {
            var playersOfClasses = new string[][]
            {
                _class1Users,
                _class2Users,
                _class3Users
            };

            for (int i = 0; i < playersOfClasses.Length; i++)
            {
                response
                    .FirstTeam
                    .Select(p => p.UserName)
                    .Intersect(playersOfClasses[i])
                    .Count()
                    .Should().Be(
                        3,
                        $"First team should have 3 " +
                        $"players of the {i + 1}th class"
                    );

                response
                    .SecondTeam
                    .Select(p => p.UserName)
                    .Intersect(playersOfClasses[i])
                    .Count()
                    .Should().Be(
                        3,
                        $"First team should have 3 " +
                        $"players of the {i + 1}th class"
                    );
            }
        }
    }
}
