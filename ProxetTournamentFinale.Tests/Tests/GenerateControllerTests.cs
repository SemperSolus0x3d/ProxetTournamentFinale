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

        private readonly PlayerDto[] _input =
        {
            // Players of first vehicle type
            new PlayerDto() {
                UserName = "_{Basil$@",
                VehicleType=1 
            },
            new PlayerDto() {
                UserName = "o-Ben#-",
                VehicleType=1 
            },
            new PlayerDto() {
                UserName = "+oBarney_+",
                VehicleType=1 
            },
            new PlayerDto() {
                UserName = "#%Gerard@%",
                VehicleType=1 
            },
            new PlayerDto() {
                UserName = "-@Grant]%",
                VehicleType=1 
            },
            new PlayerDto() {
                UserName = "<OEmil%#",
                VehicleType=1 
            },

            // Players of second vehicle type
            new PlayerDto()
            {
                UserName = "O$Mickey$@",
                VehicleType = 2
            },
            new PlayerDto()
            {
                UserName = "$-Dereko-",
                VehicleType = 2
            },
            new PlayerDto()
            {
                UserName = "o@Rusty#*",
                VehicleType = 2
            },
            new PlayerDto()
            {
                UserName = "^[Andres_$",
                VehicleType = 2
            },
            new PlayerDto()
            {
                UserName = "*<Tomas$_",
                VehicleType = 2
            },
            new PlayerDto()
            {
                UserName = "##Dwayne_o",
                VehicleType = 2
            },

            new PlayerDto()
            {
                UserName = "O#Truman#@",
                VehicleType = 3
            },
            new PlayerDto()
            {
                UserName = "*%Wesley-+",
                VehicleType = 3
            },
            new PlayerDto()
            {
                UserName = "-{Sidney_o",
                VehicleType = 3
            },
            new PlayerDto()
            {
                UserName = "[#Andre]O",
                VehicleType = 3
            },
            new PlayerDto()
            {
                UserName = "-[Oliver}@",
                VehicleType = 3
            },
            new PlayerDto()
            {
                UserName = "^[Gene*+",
                VehicleType = 3
            }
        };
    
        public async Task Post_ShouldReturnIdealTeam()
        {
            var context = new PlayersContextMock();
            var controller = new GenerateController(context);
            var input = GetTestInput();

        }

        private List<PlayerDto> GetTestInput()
        {
            var result = _input.ToList();
            Shuffle(result);
            return result;
        }

        private void Shuffle<T>(List<T> list)
        {
            Random rand = new Random();

            for (int i = list.Count; i > 1; i--)
            {
                int k = rand.Next(i + 1);

                T temp = list[k];
                list[k] = list[i];
                list[i] = temp;
            }
        }
    }
}
