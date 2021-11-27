using System;

namespace ProxetTournamentFinale.Models
{
    public class Player
    { 
        public int Id { get; set; }

        public string UserName { get; set; }

        public int VehicleType { get; set; }

        public DateTime EnqueueTime { get; set; }
    }
}