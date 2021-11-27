using ProxetTournamentFinale.Models;

namespace ProxetTournamentFinale.DataTransferObjects
{
    public class PlayerDto
    {
        public string UserName { get; set; }

        public int VehicleType { get; set; }

        public PlayerDto() { }

        public PlayerDto(Player player)
        {
            UserName = player.UserName;
            VehicleType = player.VehicleType;
        }
    }
}