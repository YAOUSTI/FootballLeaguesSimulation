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
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Match.Include(m => m.Competition).Include(m => m.Group).Include(m => m.GuestTeam).Include(m => m.HomeTeam).Include(m => m.Round);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.Competition)
                .Include(m => m.Group)
                .Include(m => m.GuestTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.Round)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Name");
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name");
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "Id", "Name");
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Name");
            ViewData["RoundId"] = new SelectList(_context.Round, "Id", "Name");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayedAt,Score1,Score2,Score1ET,Score2ET,Score1P,Score2P,Winner,HomeTeamId,GuestTeamId,CompetitionId,GroupId,RoundId")] Match match)
        {
            //If this was a group stage match this process will start storing all of match details and the standing of each team 
            var standingDetails1 = _context.TeamStanding.FirstOrDefault(s => s.TeamId == match.HomeTeamId);
            var standingDetails2 = _context.TeamStanding.FirstOrDefault(s => s.TeamId == match.GuestTeamId);

            Season season = new Season();
            Group group = new Group();
            Round round = new Round();
            group = _context.Group.FirstOrDefault(s => s.Id == match.GroupId);
            round = _context.Round.FirstOrDefault(s => s.Id == match.RoundId);

            if (standingDetails1 == null)
            {
                standingDetails1 = new TeamStanding();
                var seasonName = match.PlayedAt.Year.ToString() + "/" + (match.PlayedAt.Year + 1).ToString();
                season = _context.Season.FirstOrDefault(s => s.Name == seasonName);

                standingDetails1.TeamId = match.HomeTeamId;
                standingDetails1.SeasonId = season.Id;
                standingDetails1.CompetitionId = match.CompetitionId;
                standingDetails1.RoundId = match.RoundId;
            }

            if (standingDetails2 == null)
            {
                standingDetails2 = new TeamStanding();
                var seasonName = match.PlayedAt.Year.ToString() + "/" + (match.PlayedAt.Year + 1).ToString();
                season = _context.Season.FirstOrDefault(s => s.Name == seasonName);

                standingDetails2.TeamId = match.GuestTeamId;
                standingDetails2.SeasonId = season.Id;
                standingDetails2.CompetitionId = match.CompetitionId;
                standingDetails2.RoundId = match.RoundId;
            }

            TeamStandingsController teamStandings = new TeamStandingsController(_context);
            if (group.Name != "Null" && round.Name == "Groups")
            {
                if (standingDetails1.MatchPlayed == 6 || standingDetails2.MatchPlayed == 6)
                {
                    ViewBag.msg = "Attention! The match you are trying to create has already been played or a team has played full group matches";
                    goto SkipToEnd;
                }
                else
                {
                    teamStandings.GroupStanding(
                        standingDetails1,
                        standingDetails2,
                        match.PlayedAt,
                        match.HomeTeamId,
                        match.GuestTeamId,
                        match.Score1,
                        match.Score2,
                        match.CompetitionId,
                        (int)match.GroupId,
                        match.RoundId);
                }
                match.Winner = teamStandings.Winner;
            }

            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SkipToEnd:
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Name", match.CompetitionId);
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", match.GroupId);
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "Id", "Name", match.GuestTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Name", match.HomeTeamId);
            ViewData["RoundId"] = new SelectList(_context.Round, "Id", "Name", match.RoundId);

            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Name", match.CompetitionId);
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", match.GroupId);
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "Id", "Name", match.GuestTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Name", match.HomeTeamId);
            ViewData["RoundId"] = new SelectList(_context.Round, "Id", "Name", match.RoundId);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayedAt,Score1,Score2,Score1ET,Score2ET,Score1P,Score2P,Winner,HomeTeamId,GuestTeamId,CompetitionId,GroupId,RoundId")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Name", match.CompetitionId);
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", match.GroupId);
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "Id", "Name", match.GuestTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Name", match.HomeTeamId);
            ViewData["RoundId"] = new SelectList(_context.Round, "Id", "Name", match.RoundId);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.Competition)
                .Include(m => m.Group)
                .Include(m => m.GuestTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.Round)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Match.FindAsync(id);
            _context.Match.Remove(match);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.Id == id);
        }
    }
}
