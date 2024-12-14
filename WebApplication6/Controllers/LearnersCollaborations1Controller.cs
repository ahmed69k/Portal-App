using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class LearnersCollaborations1Controller : Controller
    {
        private readonly Fm2Context _context;

        public LearnersCollaborations1Controller(Fm2Context context)
        {
            _context = context;
        }

        // GET: LearnersCollaborations1
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Users");
            }

            var currentUserId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Fetch all collaborations for all learners
            var collaborations = await _context.LearnersCollaborations
                .Include(lc => lc.Learner)  // Optional: if you want to include learner details
                .ToListAsync();

            // Retrieve quest data related to the collaborations (joining the Quest and Collaborative tables)
            var questIds = collaborations.Select(lc => lc.QuestId).Distinct().ToList();

            var quests = await _context.Quests
                .Where(q => questIds.Contains(q.QuestId))
                .ToListAsync();

            var collaboratives = await _context.Collaboratives
                .Where(c => questIds.Contains(c.QuestId))
                .ToListAsync();

            // Retrieve users (learners) to get first and last names
            var learnerIds = collaborations.Select(lc => lc.LearnerId).Distinct().ToList();
            var learners = await _context.Learners
                .Where(u => learnerIds.Contains(u.LearnerId))
                .Select(u => new { u.LearnerId, u.FirstName, u.LastName })
                .ToListAsync();

            // Group collaborations by QuestId
            var groupedCollaborations = collaborations
                .GroupBy(lc => lc.QuestId)
                .Select(group => new
                {
                    Quest = quests.FirstOrDefault(q => q.QuestId == group.Key),
                    Learners = group.Select(lc => new
                    {
                        LearnerId = lc.LearnerId,
                        FirstName = learners.FirstOrDefault(u => u.LearnerId == lc.LearnerId)?.FirstName,
                        LastName = learners.FirstOrDefault(u => u.LearnerId == lc.LearnerId)?.LastName
                    }).ToList(),
                    Collaborative = collaboratives.FirstOrDefault(c => c.QuestId == group.Key)
                }).ToList();

            return View(groupedCollaborations);  // Pass grouped data to the view
        }






        // GET: LearnersCollaborations1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learnersCollaboration = await _context.LearnersCollaborations
                .Include(l => l.Learner)
                .Include(l => l.Quest)
                .FirstOrDefaultAsync(m => m.LearnerId == id);
            if (learnersCollaboration == null)
            {
                return NotFound();
            }

            return View(learnersCollaboration);
        }

        // GET: LearnersCollaborations1/Create
        public IActionResult Create()
        {
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId");
            ViewData["QuestId"] = new SelectList(_context.Collaboratives, "QuestId", "QuestId");
            return View();
        }

        // POST: LearnersCollaborations1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LearnerId,QuestId,CompletionStatus")] LearnersCollaboration learnersCollaboration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learnersCollaboration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", learnersCollaboration.LearnerId);
            ViewData["QuestId"] = new SelectList(_context.Collaboratives, "QuestId", "QuestId", learnersCollaboration.QuestId);
            return View(learnersCollaboration);
        }

        // GET: LearnersCollaborations1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learnersCollaboration = await _context.LearnersCollaborations.FindAsync(id);
            if (learnersCollaboration == null)
            {
                return NotFound();
            }
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", learnersCollaboration.LearnerId);
            ViewData["QuestId"] = new SelectList(_context.Collaboratives, "QuestId", "QuestId", learnersCollaboration.QuestId);
            return View(learnersCollaboration);
        }

        // POST: LearnersCollaborations1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LearnerId,QuestId,CompletionStatus")] LearnersCollaboration learnersCollaboration)
        {
            if (id != learnersCollaboration.LearnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learnersCollaboration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnersCollaborationExists(learnersCollaboration.LearnerId))
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
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", learnersCollaboration.LearnerId);
            ViewData["QuestId"] = new SelectList(_context.Collaboratives, "QuestId", "QuestId", learnersCollaboration.QuestId);
            return View(learnersCollaboration);
        }

        // GET: LearnersCollaborations1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learnersCollaboration = await _context.LearnersCollaborations
                .Include(l => l.Learner)
                .Include(l => l.Quest)
                .FirstOrDefaultAsync(m => m.LearnerId == id);
            if (learnersCollaboration == null)
            {
                return NotFound();
            }

            return View(learnersCollaboration);
        }

        // POST: LearnersCollaborations1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learnersCollaboration = await _context.LearnersCollaborations.FindAsync(id);
            if (learnersCollaboration != null)
            {
                _context.LearnersCollaborations.Remove(learnersCollaboration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearnersCollaborationExists(int id)
        {
            return _context.LearnersCollaborations.Any(e => e.LearnerId == id);
        }
    }
}
