﻿using System;
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
    public class LearnersCollaborationsController : Controller
    {
        private readonly Fm2Context _context;

        public LearnersCollaborationsController(Fm2Context context)
        {
            _context = context;
        }

        // GET: LearnersCollaborations
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Users");
            }

            var currentUserId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Fetch the current learner associated with the logged-in user
            var user = await _context.Users.Include(u => u.Learner).FirstOrDefaultAsync(u => u.Id == currentUserId);

            if (user == null || user.Learner == null || user.Role != "Learner")
            {
                return Forbid(); // Only learners should access this page
            }

            // Retrieve collaborations for the learner
            var collaborations = await _context.LearnersCollaborations
                .Where(lc => lc.LearnerId == user.LearnerId)
                .ToListAsync();

            // Retrieve quest data related to the collaborations (joining the Quest and Collaborative tables)
            var questIds = collaborations.Select(lc => lc.QuestId).ToList();

            var quests = await _context.Quests
                .Where(q => questIds.Contains(q.QuestId))
                .ToListAsync();

            var collaboratives = await _context.Collaboratives
                .Where(c => questIds.Contains(c.QuestId))
                .ToListAsync();

            // Combine the data into an anonymous object or a ViewModel (no new ViewModel class required)
            var viewModel = collaborations.Select(lc => new
            {
                lc.LearnerId,
                lc.QuestId,
                lc.CompletionStatus,
                Quest = quests.FirstOrDefault(q => q.QuestId == lc.QuestId),
                Collaborative = collaboratives.FirstOrDefault(c => c.QuestId == lc.QuestId)
            }).ToList();

            return View(viewModel);
        }




        public async Task<IActionResult> QuestMembers(int learnerId)
        {
            // Get the quest IDs the learner is part of
            var learnerQuests = await _context.LearnersCollaborations
                .Where(lc => lc.LearnerId == learnerId)
                .Select(lc => lc.QuestId)
                .Distinct()
                .ToListAsync();

            // Get all learners participating in those quests, excluding the current learner
            var otherLearners = await _context.LearnersCollaborations
                .Where(lc => learnerQuests.Contains(lc.QuestId) && lc.LearnerId != learnerId)
                .Include(lc => lc.Learner) // Include related learner data
                .Include(lc => lc.Quest)   // Include related quest data
                .Select(lc => new
                {
                    lc.LearnerId,
                    FirstName = lc.Learner.FirstName,
                    LastName = lc.Learner.LastName,
                    lc.QuestId,
                    QuestName = lc.Quest.Quest.Title, // Assuming "Title" is the quest name
                    Deadline = lc.Quest.Deadline
                })
                .ToListAsync();

            // Check if there are any other learners
            if (!otherLearners.Any())
            {
                return NotFound("No other learners are participating in the same quests as the specified learner.");
            }

            // Pass the data to the view
            return View(otherLearners);
        }











        // GET: LearnersCollaborations/Details/5
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

        // GET: LearnersCollaborations/Create
        public IActionResult Create()
        {
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId");
            ViewData["QuestId"] = new SelectList(_context.Collaboratives, "QuestId", "QuestId");
            return View();
        }

        // POST: LearnersCollaborations/Create
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

        // GET: LearnersCollaborations/Edit/5
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

        // POST: LearnersCollaborations/Edit/5
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

        public async Task<IActionResult> Delete(int learnerId, int questId)
        {
            // Find the LearnersCollaboration record for the specified learner and quest
            var learnersCollaboration = await _context.LearnersCollaborations
                .FirstOrDefaultAsync(lc => lc.LearnerId == learnerId && lc.QuestId == questId);

            // If the collaboration doesn't exist, return a NotFound response
            if (learnersCollaboration == null)
            {
                return NotFound();
            }

            // Remove the found record from the database
            _context.LearnersCollaborations.Remove(learnersCollaboration);
            await _context.SaveChangesAsync();

            // Redirect to a relevant action (e.g., the Index action for LearnersCollaborations)
            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int learnerId, int questId)
        {
            // Find the collaboration to be deleted
            var learnersCollaboration = await _context.LearnersCollaborations
                .FirstOrDefaultAsync(lc => lc.LearnerId == learnerId && lc.QuestId == questId);

            if (learnersCollaboration != null)
            {
                // Remove the collaboration from the database
                _context.LearnersCollaborations.Remove(learnersCollaboration);
                await _context.SaveChangesAsync();
            }

            // Redirect back to the index after deletion
            return RedirectToAction("Index");
        }


        private bool LearnersCollaborationExists(int id)
        {
            return _context.LearnersCollaborations.Any(e => e.LearnerId == id);
        }


        // Existing methods...

        // New method to get quest members



    }
}

    


