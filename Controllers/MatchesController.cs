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
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Id");
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Id");
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "Id", "Id");
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Id");
            ViewData["RoundId"] = new SelectList(_context.Round, "Id", "Id");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayedAt,Score1,Score2,Score1ET,Score2ET,Score1P,Score2P,Winner,HomeTeamId,GuestTeamId,CompetitionId,GroupId,RoundId")] Match match)
        {
            var standingDetails1 = _context.TeamStanding.Find(match.HomeTeamId);
            var standingDetails2 = _context.TeamStanding.Find(match.GuestTeamId); ;
            if (match.GroupId != null)
            {
                if (standingDetails1 == null)
                {
                    standingDetails1 = new TeamStanding();
                    var seasonName = match.PlayedAt.Year.ToString() + "/" + (match.PlayedAt.Year + 1).ToString();
                    var season = _context.Season.Where(s => s.Name == seasonName).ToList().First();

                    standingDetails1.TeamId = (int)match.HomeTeamId;
                    standingDetails1.SeasonId = season.Id;
                    standingDetails1.CompetitionId = match.CompetitionId;
                }
                if (standingDetails2 == null)
                {
                    standingDetails2 = new TeamStanding();
                    var seasonName = match.PlayedAt.Year.ToString() + "/" + (match.PlayedAt.Year + 1).ToString();
                    var season = _context.Season.Where(s => s.Name == seasonName).ToList().First();

                    standingDetails2.TeamId = (int)match.GuestTeamId;
                    standingDetails2.SeasonId = season.Id;
                    standingDetails2.CompetitionId = match.CompetitionId;
                }
                if (standingDetails1.MatchPlayed == 6 && standingDetails2.MatchPlayed == 6)
                {
                    return View(match);
                }
                else if (match.Score1 == match.Score2)
                {
                    standingDetails1.Draws += 1;
                    standingDetails2.Draws += 1;
                    standingDetails1.Points += 1;
                    standingDetails2.Points += 1;
                }
                else if (match.Score1 > match.Score2)
                {
                    standingDetails1.Wins += 1;
                    standingDetails2.Loses += 1;
                    standingDetails1.Points += 3;
                }
                else
                {
                    standingDetails1.Wins += 1;
                    standingDetails2.Loses += 1;
                    standingDetails1.Points += 3;
                }
                standingDetails1.MatchPlayed += 1;
                standingDetails1.GoalsFor += match.Score1;
                standingDetails1.GoalsAgaints += match.Score2;

                standingDetails2.MatchPlayed += 1;
                standingDetails2.GoalsFor += match.Score2;
                standingDetails2.GoalsAgaints += match.Score1;

                standingDetails1.GoalsDifference = standingDetails1.GoalsFor - standingDetails1.GoalsAgaints;
                standingDetails2.GoalsDifference = standingDetails2.GoalsFor - standingDetails2.GoalsAgaints;

                _context.TeamStanding.Add(standingDetails1);
                _context.Update(standingDetails2);
            }
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Id", match.CompetitionId);
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Id", match.GroupId);
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "Id", "Id", match.GuestTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Id", match.HomeTeamId);
            ViewData["RoundId"] = new SelectList(_context.Round, "Id", "Id", match.RoundId); 

           
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
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Id", match.CompetitionId);
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Id", match.GroupId);
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "Id", "Id", match.GuestTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Id", match.HomeTeamId);
            ViewData["RoundId"] = new SelectList(_context.Round, "Id", "Id", match.RoundId);
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
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Id", match.CompetitionId);
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Id", match.GroupId);
            ViewData["GuestTeamId"] = new SelectList(_context.Team, "Id", "Id", match.GuestTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Id", match.HomeTeamId);
            ViewData["RoundId"] = new SelectList(_context.Round, "Id", "Id", match.RoundId);
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
