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
        private readonly ApplicationDbContext _context;

        public TeamStandingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeamStandings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeamStanding.Include(t => t.Competition).Include(t => t.Season).Include(t => t.Team);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TeamStandings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamStanding = await _context.TeamStanding
                .Include(t => t.Competition)
                .Include(t => t.Season)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamStanding == null)
            {
                return NotFound();
            }

            return View(teamStanding);
        }

        // GET: TeamStandings/Create
        public IActionResult Create()
        {
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Id");
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Id");
            return View();
        }

        // POST: TeamStandings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MatchPlayed,Wins,Loses,Draws,GoalsFor,GoalsAgaints,GoalsDifference,Points,TeamId,SeasonId,CompetitionId")] TeamStanding teamStanding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamStanding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Id", teamStanding.CompetitionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", teamStanding.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Id", teamStanding.TeamId);
            return View(teamStanding);
        }

        // GET: TeamStandings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamStanding = await _context.TeamStanding.FindAsync(id);
            if (teamStanding == null)
            {
                return NotFound();
            }
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Id", teamStanding.CompetitionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", teamStanding.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Id", teamStanding.TeamId);
            return View(teamStanding);
        }

        // POST: TeamStandings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MatchPlayed,Wins,Loses,Draws,GoalsFor,GoalsAgaints,GoalsDifference,Points,TeamId,SeasonId,CompetitionId")] TeamStanding teamStanding)
        {
            if (id != teamStanding.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamStanding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamStandingExists(teamStanding.Id))
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
            ViewData["CompetitionId"] = new SelectList(_context.Competition, "Id", "Id", teamStanding.CompetitionId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", teamStanding.SeasonId);
            ViewData["TeamId"] = new SelectList(_context.Team, "Id", "Id", teamStanding.TeamId);
            return View(teamStanding);
        }

        // GET: TeamStandings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamStanding = await _context.TeamStanding
                .Include(t => t.Competition)
                .Include(t => t.Season)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamStanding == null)
            {
                return NotFound();
            }

            return View(teamStanding);
        }

        // POST: TeamStandings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamStanding = await _context.TeamStanding.FindAsync(id);
            _context.TeamStanding.Remove(teamStanding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamStandingExists(int id)
        {
            return _context.TeamStanding.Any(e => e.Id == id);
        }
    }
}
