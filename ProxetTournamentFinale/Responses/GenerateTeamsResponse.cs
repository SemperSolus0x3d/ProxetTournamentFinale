using ProxetTournamentFinale.DataTransferObjects;

namespace ProxetTournamentFinale.Responses
{
    public class GenerateTeamsResponse
    {
        public PlayerDto[] FirstTeam { get; set; }
        public PlayerDto[] SecondTeam { get; set; }
    }
}