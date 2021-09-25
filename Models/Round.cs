using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeaguesSimulation.Models
{
    public class Round
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }

        //A round belongs to a competition
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        //A round can have more than 1 match (has many)
        public virtual ICollection<Match> Matches { get; set; }
        //A round have a list of Standings
        public virtual ICollection<TeamStanding> TeamStandings { get; set; }
    }
}
