using System;

using ProxetTournamentFinale.DataTransferObjects;

namespace ProxetTournamentFinale.Models
{
    public class Player
    { 
        public int Id { get; set; }

        public string UserName { get; set; }

        public int VehicleType { get; set; }

        public DateTime EnqueueTime { get; set; }

        public Player(PlayerDto dto)
        {
            // As far as I know,
            // the database handles
            // Id property by itself
            // so it's pointless
            // to set it here
            Id = 0;

            UserName = dto.UserName;
            VehicleType = dto.VehicleType;

            EnqueueTime = DateTime.UtcNow;
        }
    }
}