using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeaguesSimulation.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //A groupcan be in a competiton
        public int? CompetitionId { get; set; }
        public Competition Competition { get; set; }
        //A group can have more than 1 team (has many)
        public List<Team> Teams { get; set; }
        //A group can have more than 1 match (has many)
        public List<Match> Matches { get; set; }
    }
}
