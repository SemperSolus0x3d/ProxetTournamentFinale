using ProxetTournamentFinale.DataTransferObjects;

namespace ProxetTournamentFinale.Responses
{
    public class GenerateTeamsResponse
    {
        public PlayerDto[] FirstTeam { get; set; }
        public PlayerDto[] SecondTeam { get; set; }

        public GenerateTeamsResponse(PlayerDto[] team1, PlayerDto[] team2)
        {
            FirstTeam = team1;
            SecondTeam = team2;
        }
    }
}