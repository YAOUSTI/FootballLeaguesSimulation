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
        public int Winner = 0;
        public int Agg1 { get; set; }
        public int Agg2 { get; set; }
        
        public int GroupStanding(TeamStanding standingDetails1, TeamStanding standingDetails2, DateTime playedAt, int homeTeam, int guestTeam, int score1, int score2, int competition, int group, int round)
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
            return Winner;
        }
        public int PlayOffFirstLeg(int hometeam, int guestteam, int score1, int score2)
        {
            if (score1 > score2) Winner = hometeam;
            else if (score1 < score2) Winner = guestteam;
            Agg1 = score1;
            Agg2 = score2;

            return Winner;
        }
        public int PlayOffSecondLeg(int hometeam, int guestteam, int score1, int score2, int competition, int round)
        {
            var matchLeg1 = _context.Match.FirstOrDefault(s => s.HomeTeamId == guestteam && s.GuestTeamId == hometeam && s.Leg == 1 && s.RoundId == round && s.CompetitionId == competition);
            Agg1 = matchLeg1.Aggregation1 + score1;
            Agg2 = matchLeg1.Aggregation2 + score2;
            if (Agg1 == Agg2)
            {
                if (matchLeg1.Score2 > score2) Winner = hometeam;
                if (matchLeg1.Score2 < score2) Winner = guestteam;
            }
            else if (Agg1 > Agg2)
            {
                Winner = hometeam;
            }
            else
            {
                Winner = guestteam;
            }

            return Winner;
        }

        public int PlayOffSecondLegEquality(int hometeam, int guestteam, int extratime1, int extratime2)
        {
            if (extratime1 != 0 && extratime2 != 0 && extratime1 == extratime2)
            {
                Winner = guestteam;
                Agg1 += extratime1;
                Agg2 += extratime2;
            }
            else if (extratime1 > extratime2)
            {
                Winner = hometeam;
                Agg1 += extratime1;
                Agg2 += extratime2;
            }
            else if (extratime1 < extratime2)
            {
                Winner = guestteam;
                Agg1 += extratime1;
                Agg2 += extratime2;
            }
            return Winner;
        }
        public int PlayOffSecondLegEqualityExtraTimes(int hometeam, int guestteam, int penalties1, int penalties2)
        {
            if (penalties1 > penalties2) Winner = hometeam;
            else Winner = guestteam;

            return Winner;
        }


        public int Final(int hometeam, int guestteam, int score1, int score2)
        {
            if (score1 > score2) Winner = hometeam;
            else if (score1 < score2) Winner = guestteam;

            return Winner;
        }

        public int FinalEquality(int hometeam, int guestteam, int extratime1, int extratime2)
        {
            if (extratime1 > extratime2)
            {
                Winner = hometeam;
            }
            else if (extratime1 < extratime2)
            {
                Winner = guestteam;
            }
            return Winner;
        }
        public int FinalEqualityExtraTimes(int hometeam, int guestteam, int penalties1, int penalties2)
        {
            if (penalties1 > penalties2) Winner = hometeam;
            else Winner = guestteam;

            return Winner;
        }
    }
}