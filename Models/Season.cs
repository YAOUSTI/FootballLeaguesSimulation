using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeaguesSimulation.Models
{
    public class Season
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // A lot of competitions can be played in 1 Season
        public virtual ICollection<Competition> Competitions { get; set; }
        //A Season have a list Standings
        public virtual ICollection<TeamStanding> TeamStandings { get; set; }
    }
}
