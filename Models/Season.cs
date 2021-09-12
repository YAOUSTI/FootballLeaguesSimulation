using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<Competition> Competitions { get; set; }
    }
}
