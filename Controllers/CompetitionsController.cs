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
    public class CompetitionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompetitionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Competitions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Competition.Include(c => c.League).Include(c => c.Season);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Competitions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition
                .Include(c => c.League)
                .Include(c => c.Season)
                .Include(c => c.Rounds)
                .Include(c => c.Groups).ThenInclude(c => c.Teams).ThenInclude(c => c.TeamStandings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // GET: Competitions/Create
        public IActionResult Create()
        {
            ViewData["LeagueId"] = new SelectList(_context.League, "Id", "Id");
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id");
            return View();
        }

        // POST: Competitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Logo,StartAt,EndAt,SeasonId,LeagueId")] Competition competition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(competition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeagueId"] = new SelectList(_context.League, "Id", "Id", competition.LeagueId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", competition.SeasonId);
            return View(competition);
        }

        // GET: Competitions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }
            ViewData["LeagueId"] = new SelectList(_context.League, "Id", "Id", competition.LeagueId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", competition.SeasonId);
            return View(competition);
        }

        // POST: Competitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Logo,StartAt,EndAt,SeasonId,LeagueId")] Competition competition)
        {
            if (id != competition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(competition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompetitionExists(competition.Id))
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
            ViewData["LeagueId"] = new SelectList(_context.League, "Id", "Id", competition.LeagueId);
            ViewData["SeasonId"] = new SelectList(_context.Season, "Id", "Id", competition.SeasonId);
            return View(competition);
        }

        // GET: Competitions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var competition = await _context.Competition
                .Include(c => c.League)
                .Include(c => c.Season)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (competition == null)
            {
                return NotFound();
            }

            return View(competition);
        }

        // POST: Competitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var competition = await _context.Competition.FindAsync(id);
            _context.Competition.Remove(competition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competition.Any(e => e.Id == id);
        }
    }
}
