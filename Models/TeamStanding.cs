using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeaguesSimulation.Models
{
    public class TeamStanding
    {
        [Key]
        public int Id { get; set; }
        public int MatchPlayed { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Draws { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgaints { get; set; }
        public int GoalsDifference { get; set; }
        public int Points { get; set; }
        public int Leg { get; set; }
        public bool Qualification { get; set; }

        //Standings belongs to a team
        public int TeamId { get; set; }
        public Team Team { get; set; }

        //Standings belongs to a season
        public int SeasonId { get; set; }
        public Season Season { get; set; }

        //Standings belongs to a competition
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        //Standings belongs to a round
        public int RoundId { get; set; }
        public Round Round { get; set; }
    }
}
