using FootballLeaguesSimulation.Data;
using FootballLeaguesSimulation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballLeaguesSimulation.Controllers
{
    public class TeamStandingsController : Controller
    {
        private ApplicationDbContext _context;


        public TeamStandingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Winner { get; set; }
        public void GroupStanding(TeamStanding standingDetails1, TeamStanding standingDetails2, DateTime playedAt, int homeTeam, int guestTeam, int score1, int score2, int competition, int group, int round)
        {
            if (score1 == score2)
            {
                standingDetails1.Draws += 1;
                standingDetails2.Draws += 1;
                standingDetails1.Points += 1;
                standingDetails2.Points += 1;
            }
            if (score1 > score2)
            {
                standingDetails1.Wins += 1;
                standingDetails1.Points += 3;
                standingDetails2.Loses += 1;
                Winner = homeTeam;
            }
            if (score1 < score2)
            {
                standingDetails1.Loses += 1;
                standingDetails2.Wins += 1;
                standingDetails2.Points += 3;
                Winner = guestTeam;
            }
            standingDetails1.MatchPlayed += 1;
            standingDetails1.GoalsFor += score1;
            standingDetails1.GoalsAgaints += score2;

            standingDetails2.MatchPlayed += 1;
            standingDetails2.GoalsFor += score2;
            standingDetails2.GoalsAgaints += score1;

            standingDetails1.GoalsDifference = standingDetails1.GoalsFor - standingDetails1.GoalsAgaints;
            standingDetails2.GoalsDifference = standingDetails2.GoalsFor - standingDetails2.GoalsAgaints;

            standingDetails1.GroupId = group;
            standingDetails2.GroupId = group;

            var tmpGroupStandingsList = _context.TeamStanding
            .Where(g => g.GroupId == group && g.TeamId != homeTeam && g.TeamId != guestTeam).ToList();
            tmpGroupStandingsList.Add(standingDetails1);
            tmpGroupStandingsList.Add(standingDetails2);

            for (int i = 0; i < tmpGroupStandingsList.Count - 1; i++)
            {
                for (int j = i + 1; j < tmpGroupStandingsList.Count; j++)
                {
                    if (tmpGroupStandingsList[i].Points < tmpGroupStandingsList[j].Points)
                    {
                        tmpGroupStandingsList[i].Rank = j + 1;
                        var tmpStanding = tmpGroupStandingsList[i];
                        tmpGroupStandingsList[j].Rank = i + 1;
                        tmpGroupStandingsList[i] = tmpGroupStandingsList[j];
                        tmpGroupStandingsList[j] = tmpStanding;
                    }
                    else if (tmpGroupStandingsList[i].Points == tmpGroupStandingsList[j].Points && tmpGroupStandingsList[i].GoalsDifference < tmpGroupStandingsList[j].GoalsDifference)
                    {
                        tmpGroupStandingsList[i].Rank = j + 1;
                        var tmpStanding = tmpGroupStandingsList[i];
                        tmpGroupStandingsList[j].Rank = i + 1;
                        tmpGroupStandingsList[i] = tmpGroupStandingsList[j];
                        tmpGroupStandingsList[j] = tmpStanding;
                    }
                    else if (tmpGroupStandingsList[i].Points == tmpGroupStandingsList[j].Points 
                        && tmpGroupStandingsList[i].GoalsDifference == tmpGroupStandingsList[j].GoalsDifference 
                        && tmpGroupStandingsList[i].GoalsFor < tmpGroupStandingsList[j].GoalsFor)
                    {
                        tmpGroupStandingsList[i].Rank = j + 1;
                        var tmpStanding = tmpGroupStandingsList[i];
                        tmpGroupStandingsList[j].Rank = i + 1;
                        tmpGroupStandingsList[i] = tmpGroupStandingsList[j];
                        tmpGroupStandingsList[j] = tmpStanding;
                    }
                }
            }
            foreach (var standing in tmpGroupStandingsList)
            {
                _context.TeamStanding.Update(standing);
            }
        }
        //public void EleminationStanding(TeamStanding standingDetails1, TeamStanding standingDetails2, DateTime playedat, int hometeam, int guestteam, int score1, int score2, int extratime1, int extratime2, int penalties1, int penalties2, int agg1, int agg2, int competition, int round, int leg)
        //{
        //    if (leg == 1)
        //    {
        //        if (score1 > score2) Winner = hometeam;
        //        else Winner = guestteam;
        //    }
        //    if (leg == 2 && agg1 == agg2)
        //    {
        //        var matchLeg1 = _context.Match.FirstOrDefault(s => s.HomeTeamId == hometeam && s.GuestTeamId == guestteam && s.Leg == 1 && s.RoundId == round && s.CompetitionId == competition);
        //        if (matchLeg1.Score2 > score2) Winner = guestteam;
        //        if (matchLeg1.Score2 < score2) Winner = hometeam;
        //        if (matchLeg1.Score2 == score2)
        //        {
        //            if (extratime1 == 0 && extratime2 == 0)
        //            {
        //                if (penalties1 > penalties2) Winner = hometeam;
        //                else Winner = guestteam;
        //            }
        //            else if (extratime1 != 0 && extratime2 != 0 && extratime1 == extratime2) Winner = guestteam;
        //            else if (extratime1 > extratime2) Winner = hometeam;
        //            else Winner = guestteam;
        //        }
        //    }
        //    else if (leg == 2 && agg1 > agg2) Winner = hometeam;
        //    else Winner = guestteam;

        //    _context.TeamStanding.Update(standingDetails1);
        //    _context.TeamStanding.Update(standingDetails2);
        //}
    }
}
