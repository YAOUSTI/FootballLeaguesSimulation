using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FootballLeaguesSimulation.Models;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FootballLeaguesSimulation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FootballLeaguesSimulation.Models.League> League { get; set; }
        public DbSet<FootballLeaguesSimulation.Models.Season> Season { get; set; }
        public DbSet<FootballLeaguesSimulation.Models.Competition> Competition { get; set; }
        public DbSet<FootballLeaguesSimulation.Models.Round> Round { get; set; }
        public DbSet<FootballLeaguesSimulation.Models.Group> Group { get; set; }
        public DbSet<FootballLeaguesSimulation.Models.Team> Team { get; set; }
        public DbSet<FootballLeaguesSimulation.Models.Match> Match { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<League>().HasData(
                    new League { Id = 1, Name = "EUFA" }
                );
            builder.Entity<Season>().HasData(
                    new League { Id = 1, Name = "2017/2018" }
                );
            builder.Entity<Competition>().HasData(
                    new Competition { 
                        Id = 1, 
                        Name = "UEFA CHAMPIONS LEAGUE", 
                        Logo= "https://e7.pngegg.com/pngimages/115/195/png-clipart-graphics-uefa-europa-league-logo-football-football-uefa-europa-league-logo.png",
                        SeasonId = 1,
                        LeagueId = 1
                    }
                );
            builder.Entity<Round>().HasData(
                    new Round { Id = 1, Name = "Groups", CompetitionId = 1 },
                    new Round { Id = 2, Name = "Round of 16", CompetitionId = 1 },
                    new Round { Id = 3, Name = "Round of 8", CompetitionId = 1 },
                    new Round { Id = 4, Name = "Round of 4", CompetitionId = 1 },
                    new Round { Id = 5, Name = "Final", CompetitionId = 1 }
                );
            builder.Entity<Group>().HasData(
                    new Group { Id = 1, Name = "Group A", CompetitionId = 1 },
                    new Group { Id = 2, Name = "Group B", CompetitionId = 1 },
                    new Group { Id = 3, Name = "Group C", CompetitionId = 1 },
                    new Group { Id = 4, Name = "Group D", CompetitionId = 1 },
                    new Group { Id = 5, Name = "Group E", CompetitionId = 1 },
                    new Group { Id = 6, Name = "Group F", CompetitionId = 1 },
                    new Group { Id = 7, Name = "Group G", CompetitionId = 1 },
                    new Group { Id = 8, Name = "Group H", CompetitionId = 1 }
                );
            var j = 1;
            using (StreamReader r = new StreamReader(@"Data/groups.json"))
            {
                string json = r.ReadToEnd();
                var items = JObject.Parse(json);
                foreach (var group in items["groups"])
                {
                    foreach (var team in group["teams"])
                    {
                        builder.Entity<Team>().HasData(
                            new Team
                            {
                                Id = j++,
                                Name = team["name"].ToString(),
                                Code = team["code"].ToString(),
                                GroupId = ((int)group["id"]),
                                Logo = team["logo"].ToString(),
                                CompetitionId = 1,
                            }
                        );
                    }


                }
            }

            var k = 1;
            using (StreamReader r = new StreamReader(@"Data/matchs.json"))
            {
                string json = r.ReadToEnd();
                var items = JObject.Parse(json);
                foreach (var match in items["matchs"])
                {
                    var w = 0;
                    if((int)match["score_1"] > (int)match["score_2"]){
                        w = (int)match["team_home"]["id"];
                    }
                    else if((int)match["score_1"] < (int)match["score_2"])
                    {
                        w = (int)match["team_away"]["id"];
                    }
                    else
                    {
                        w = 0;
                    }

                    builder.Entity<Match>().HasData(
                        new Match
                        {
                            Id = k++,
                            HomeTeamId = ((int)match["team_home"]["id"]),
                            GuestTeamId = ((int)match["team_away"]["id"]),
                            Score1 = ((int)match["score_1"]),
                            Score2 = ((int)match["score_2"]),
                            Score1ET = ((int?)match["score_extra_1"]),
                            Score2ET = ((int?)match["score_extra_2"]),
                            Score1P = ((int?)match["penalties_1"]),
                            Score2P = ((int?)match["penalties_2"]),
                            CompetitionId = 1,
                            GroupId = ((int)match["team_home"]["group_id"]),
                            RoundId = 1,
                            PlayedAt = ((DateTime)match["played_at"]),
                            Winner = w,
                        }
                    );
                }
            }
        }
    }
}
