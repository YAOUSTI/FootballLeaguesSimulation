using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FootballLeaguesSimulation.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // A League can have 1 or more Competitions
        public List<Competition> Competitions { get; set; }
    }
}
