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
    public class CollaborativesController : Controller
    {
        private readonly Fm2Context _context;

        public CollaborativesController(Fm2Context context)
        {
            _context = context;
        }

        // GET: Collaboratives
        public async Task<IActionResult> Index()
        {
            var fm2Context = _context.Collaboratives.Include(c => c.Quest);
            return View(await fm2Context.ToListAsync());
        }

        // GET: Collaboratives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborative = await _context.Collaboratives
                .Include(c => c.Quest)
                .FirstOrDefaultAsync(m => m.QuestId == id);
            if (collaborative == null)
            {
                return NotFound();
            }

            return View(collaborative);
        }

        // GET: Collaboratives/Create
        public IActionResult Create()
        {
            ViewData["QuestId"] = new SelectList(_context.Quests, "QuestId", "QuestId");
            return View();
        }

        // POST: Collaboratives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestId,Deadline,MaxNumParticipants")] Collaborative collaborative)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collaborative);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestId"] = new SelectList(_context.Quests, "QuestId", "QuestId", collaborative.QuestId);
            return View(collaborative);
        }

        // GET: Collaboratives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborative = await _context.Collaboratives.FindAsync(id);
            if (collaborative == null)
            {
                return NotFound();
            }
            ViewData["QuestId"] = new SelectList(_context.Quests, "QuestId", "QuestId", collaborative.QuestId);
            return View(collaborative);
        }

        // POST: Collaboratives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestId,Deadline,MaxNumParticipants")] Collaborative collaborative)
        {
            if (id != collaborative.QuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collaborative);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaborativeExists(collaborative.QuestId))
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
            ViewData["QuestId"] = new SelectList(_context.Quests, "QuestId", "QuestId", collaborative.QuestId);
            return View(collaborative);
        }

        // GET: Collaboratives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborative = await _context.Collaboratives
                .Include(c => c.Quest)
                .FirstOrDefaultAsync(m => m.QuestId == id);
            if (collaborative == null)
            {
                return NotFound();
            }

            return View(collaborative);
        }

        // POST: Collaboratives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collaborative = await _context.Collaboratives.FindAsync(id);
            if (collaborative != null)
            {
                _context.Collaboratives.Remove(collaborative);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollaborativeExists(int id)
        {
            return _context.Collaboratives.Any(e => e.QuestId == id);
        }
    }
}
