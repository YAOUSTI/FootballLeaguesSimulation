using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeaguesSimulation.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public DateTime PlayedAt { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public int? Score1ET { get; set; }
        public int? Score2ET { get; set; }
        public int? Score1P { get; set; }
        public int? Score2P { get; set; }
        public int Winner { get; set; }

        
        //A match belongs to two teams
        public int HomeTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }
        public int GuestTeamId { get; set; }
        public virtual Team GuestTeam { get; set; }

        //A match belongs to a competiton
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        //A match belongs to a group
        public int? GroupId { get; set; }
        public Group Group { get; set; }

        //A match belongs to a round
        public int? RoundId { get; set; }
        public Round Round { get; set; }
    }
}
