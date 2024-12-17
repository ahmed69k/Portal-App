using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class RankingsController : Controller
    {
        private readonly Fm2Context _context;

        public RankingsController(Fm2Context context)
        {
            _context = context;
        }

        // GET: Rankings
        public async Task<IActionResult> Index()
        {
            var fm2Context = _context.Rankings.Include(r => r.Board).Include(r => r.Course).Include(r => r.Learner);
            return View(await fm2Context.ToListAsync());
        }

        // GET: Rankings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ranking = await _context.Rankings
                .Include(r => r.Board)
                .Include(r => r.Course)
                .Include(r => r.Learner)
                .FirstOrDefaultAsync(m => m.BoardId == id);
            if (ranking == null)
            {
                return NotFound();
            }

            return View(ranking);
        }

        // GET: Rankings/Create
        public IActionResult Create()
        {
            ViewData["BoardId"] = new SelectList(_context.Leaderboards, "BoardId", "BoardId");
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId");
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId");
            return View();
        }

        // POST: Rankings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BoardId,LearnerId,CourseId,Rank,TotalPoints")] Ranking ranking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ranking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardId"] = new SelectList(_context.Leaderboards, "BoardId", "BoardId", ranking.BoardId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", ranking.CourseId);
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", ranking.LearnerId);
            return View(ranking);
        }

        // GET: Rankings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ranking = await _context.Rankings.FindAsync(id);
            if (ranking == null)
            {
                return NotFound();
            }
            ViewData["BoardId"] = new SelectList(_context.Leaderboards, "BoardId", "BoardId", ranking.BoardId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", ranking.CourseId);
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", ranking.LearnerId);
            return View(ranking);
        }

        // POST: Rankings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BoardId,LearnerId,CourseId,Rank,TotalPoints")] Ranking ranking)
        {
            if (id != ranking.BoardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ranking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankingExists(ranking.BoardId))
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
            ViewData["BoardId"] = new SelectList(_context.Leaderboards, "BoardId", "BoardId", ranking.BoardId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", ranking.CourseId);
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", ranking.LearnerId);
            return View(ranking);
        }

        // GET: Rankings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ranking = await _context.Rankings
                .Include(r => r.Board)
                .Include(r => r.Course)
                .Include(r => r.Learner)
                .FirstOrDefaultAsync(m => m.BoardId == id);
            if (ranking == null)
            {
                return NotFound();
            }

            return View(ranking);
        }

        // POST: Rankings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ranking = await _context.Rankings.FindAsync(id);
            if (ranking != null)
            {
                _context.Rankings.Remove(ranking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankingExists(int id)
        {
            return _context.Rankings.Any(e => e.BoardId == id);
        }
    }
}
