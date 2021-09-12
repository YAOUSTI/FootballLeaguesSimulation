using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeaguesSimulation.Models
{
    public class Competition
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        

        //A competition can have more than 1 match (has many)
        public List<Match> Matches { get; set; }
        //A competition can have more than 1 team (has many)
        public List<Team> Teams { get; set; }
        //A competition can have more than 1 group (has many)
        public List<Group> Groups { get; set; }
        //A competition can have more than 1 round (has many)
        public List<Round> Rounds { get; set; }
        //A Competition can be played in 1 Season (has One)
        public int SeasonId { get; set; }
        public Season Season { get; set; }
        //A Competition belongs to a League
        public int LeagueId { get; set; }
        public League League { get; set; }
    }
}
