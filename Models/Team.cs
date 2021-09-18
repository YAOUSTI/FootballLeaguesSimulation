using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeaguesSimulation.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Logo { get; set; }

        //A team can have matches home / away (has many)
        [InverseProperty("HomeTeam")]
        public virtual ICollection<Match> HomeMatches { get; set; }

        [InverseProperty("GuestTeam")]
        public virtual ICollection<Match> GuestMatches { get; set; }

        //A Team can play a Competition
        public int? CompetitionId { get; set; }
        public Competition Competition { get; set; }
        //A team can be in a group
        public int? GroupId { get; set; }
        public Group Group { get; set; }
        //A team can have Standings
        public virtual ICollection<TeamStanding> TeamStandings { get; set; }
    }
}
