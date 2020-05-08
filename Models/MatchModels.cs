using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MatchData
    {
        public string MatchName { get; set; }
        public string MatchDate { get; set; }
        public string TeamOne { get; set; }
        public string TeamTwo { get; set; }
        public string StartTime { get; set; }
        public string MatchAddress { get; set; }

        public List<PlayersData> lstPlayers { get; set; }
    }

    public class PlayersData
    {
        public string PlayerName { get; set; }
        public string PlayerType { get; set; }
        public string PlayerPosition { get; set; }
        public string PlayerTeam { get; set; }
        public string MatchId { get; set; }
    }
}
