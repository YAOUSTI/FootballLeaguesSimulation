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
