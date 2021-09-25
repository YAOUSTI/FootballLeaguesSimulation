using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballLeaguesSimulation.Data;
using FootballLeaguesSimulation.Models;

namespace FootballLeaguesSimulation.Controllers
{
    public class TeamStandingsController : Controller
    {
        private ApplicationDbContext _context;


        public TeamStandingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public void GroupStanding(TeamStanding standingDetails1, TeamStanding standingDetails2, DateTime playedAt, int homeTeam, int guestTeam, int score1, int score2, int competition,int round)
        {
            if (score1 == score2)
            {
                standingDetails1.Draws += 1;
                standingDetails2.Draws += 1;
                standingDetails1.Points += 1;
                standingDetails2.Points += 1;
            }
            else if (score1 > score2)
            {
                standingDetails1.Wins += 1;
                standingDetails2.Loses += 1;
                standingDetails1.Points += 3;
            }
            else
            {
                standingDetails1.Loses += 1;
                standingDetails2.Wins += 1;
                standingDetails1.Points += 3;
            }
            standingDetails1.MatchPlayed += 1;
            standingDetails1.GoalsFor += score1;
            standingDetails1.GoalsAgaints += score2;

            standingDetails2.MatchPlayed += 1;
            standingDetails2.GoalsFor += score2;
            standingDetails2.GoalsAgaints += score1;

            standingDetails1.GoalsDifference = standingDetails1.GoalsFor - standingDetails1.GoalsAgaints;
            standingDetails2.GoalsDifference = standingDetails2.GoalsFor - standingDetails2.GoalsAgaints;

            _context.TeamStanding.Update(standingDetails1);
            _context.TeamStanding.Update(standingDetails2);
        }
        public void EleminationStanding(TeamStanding standingDetails1, TeamStanding standingDetails2, DateTime playedAt, int homeTeam, int guestTeam, int winner, int score1, int score2, int extraTime1, int extraTime2, int penalties1, int penalties2, int competition, int round,int leg)
        {
            if (score1 == score2)
            {
                standingDetails1.Draws += 1;
                standingDetails2.Draws += 1;
            }
            else if (score1 > score2)
            {
                standingDetails1.Wins += 1;
                standingDetails2.Loses += 1;
                standingDetails1.Points += 3;
            }
            else
            {
                standingDetails1.Loses += 1;
                standingDetails2.Wins += 1;
                standingDetails1.Points += 3;
            }
            standingDetails1.MatchPlayed += 1;
            standingDetails1.GoalsFor += score1;
            standingDetails1.GoalsAgaints += score2;

            standingDetails2.MatchPlayed += 1;
            standingDetails2.GoalsFor += score2;
            standingDetails2.GoalsAgaints += score1;

            standingDetails1.GoalsDifference = standingDetails1.GoalsFor - standingDetails1.GoalsAgaints;
            standingDetails2.GoalsDifference = standingDetails2.GoalsFor - standingDetails2.GoalsAgaints;

            _context.TeamStanding.Update(standingDetails1);
            _context.TeamStanding.Update(standingDetails2);
        SkipToEnd:;
        }
    }
}
