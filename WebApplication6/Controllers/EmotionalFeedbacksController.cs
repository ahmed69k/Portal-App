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
    public class EmotionalFeedbacksController : Controller
    {
        private readonly Fm2Context _context;

        public EmotionalFeedbacksController(Fm2Context context)
        {
            _context = context;
        }

        // GET: EmotionalFeedbacks
        public async Task<IActionResult> Index()
        {
            var fm2Context = _context.EmotionalFeedbacks.Include(e => e.Activity).Include(e => e.Learner);
            return View(await fm2Context.ToListAsync());
        }

        // GET: EmotionalFeedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emotionalFeedback = await _context.EmotionalFeedbacks
                .Include(e => e.Activity)
                .Include(e => e.Learner)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (emotionalFeedback == null)
            {
                return NotFound();
            }

            return View(emotionalFeedback);
        }

        // GET: EmotionalFeedbacks/Create
        public IActionResult Create()
        {
            ViewData["ActivityId"] = new SelectList(_context.LearningActivities, "ActivityId", "ActivityId");
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId");
            return View();
        }

        // POST: EmotionalFeedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeedbackId,LearnerId,ActivityId,Timestamp,EmotionalState")] EmotionalFeedback emotionalFeedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emotionalFeedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityId"] = new SelectList(_context.LearningActivities, "ActivityId", "ActivityId", emotionalFeedback.ActivityId);
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", emotionalFeedback.LearnerId);
            return View(emotionalFeedback);
        }

        // GET: EmotionalFeedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emotionalFeedback = await _context.EmotionalFeedbacks.FindAsync(id);
            if (emotionalFeedback == null)
            {
                return NotFound();
            }
            ViewData["ActivityId"] = new SelectList(_context.LearningActivities, "ActivityId", "ActivityId", emotionalFeedback.ActivityId);
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", emotionalFeedback.LearnerId);
            return View(emotionalFeedback);
        }

        // POST: EmotionalFeedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedbackId,LearnerId,ActivityId,Timestamp,EmotionalState")] EmotionalFeedback emotionalFeedback)
        {
            if (id != emotionalFeedback.FeedbackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emotionalFeedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmotionalFeedbackExists(emotionalFeedback.FeedbackId))
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
            ViewData["ActivityId"] = new SelectList(_context.LearningActivities, "ActivityId", "ActivityId", emotionalFeedback.ActivityId);
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", emotionalFeedback.LearnerId);
            return View(emotionalFeedback);
        }

        // GET: EmotionalFeedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emotionalFeedback = await _context.EmotionalFeedbacks
                .Include(e => e.Activity)
                .Include(e => e.Learner)
                .FirstOrDefaultAsync(m => m.FeedbackId == id);
            if (emotionalFeedback == null)
            {
                return NotFound();
            }

            return View(emotionalFeedback);
        }

        // POST: EmotionalFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emotionalFeedback = await _context.EmotionalFeedbacks.FindAsync(id);
            if (emotionalFeedback != null)
            {
                _context.EmotionalFeedbacks.Remove(emotionalFeedback);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmotionalFeedbackExists(int id)
        {
            return _context.EmotionalFeedbacks.Any(e => e.FeedbackId == id);
        }
    }
}
