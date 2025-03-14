﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class TakenassessmentsController : Controller
    {
        private readonly Fm2Context _context;

        public TakenassessmentsController(Fm2Context context)
        {
            _context = context;
        }

        // GET: Takenassessments
        public async Task<IActionResult> Index()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); // Return 401 if user is not authenticated
            }

            // Retrieve the current user's ID from the claims
            var currentUserId = int.Parse(User.FindFirst("Id")?.Value ?? "0");

            // Fetch the current user and ensure the Learner relationship is included
            var user = await _context.Users
                .Include(u => u.Learner)
                .FirstOrDefaultAsync(u => u.Id == currentUserId);

            if (user?.Learner == null)
            {
                return NotFound(); // If no learner associated with the user, return 404
            }

            // Query assessment scores for the specific learner
            var scores = await _context.Takenassessments
                .Include(t => t.Assessment) // Ensure Assessment is loaded
                .Where(t => t.LearnerId == user.Learner.LearnerId) // Filter by the learner's ID
                .Select(t => new
                {
                    Title = t.Assessment.Title, // Access the correct Title property
                    Score = t.ScoredPoint,
                    TotalMarks = t.Assessment.TotalMarks,
                    LearnerId = t.LearnerId
                })
                .ToListAsync();

            return View(scores); 
        }

        // GET: Takenassessments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var takenassessment = await _context.Takenassessments
                .Include(t => t.Assessment)
                .Include(t => t.Learner)
                .FirstOrDefaultAsync(m => m.AssessmentId == id);
            if (takenassessment == null)
            {
                return NotFound();
            }

            return View(takenassessment);
        }

        // GET: Takenassessments/Create
        public IActionResult Create()
        {
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "Id", "Id");
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId");
            return View();
        }

        // POST: Takenassessments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssessmentId,LearnerId,ScoredPoint")] Takenassessment takenassessment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(takenassessment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "Id", "Id", takenassessment.AssessmentId);
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", takenassessment.LearnerId);
            return View(takenassessment);
        }

        // GET: Takenassessments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var takenassessment = await _context.Takenassessments.FindAsync(id);
            if (takenassessment == null)
            {
                return NotFound();
            }
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "Id", "Id", takenassessment.AssessmentId);
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", takenassessment.LearnerId);
            return View(takenassessment);
        }

        // POST: Takenassessments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssessmentId,LearnerId,ScoredPoint")] Takenassessment takenassessment)
        {
            if (id != takenassessment.AssessmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(takenassessment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TakenassessmentExists(takenassessment.AssessmentId))
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
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "Id", "Id", takenassessment.AssessmentId);
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", takenassessment.LearnerId);
            return View(takenassessment);
        }

        // GET: Takenassessments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var takenassessment = await _context.Takenassessments
                .Include(t => t.Assessment)
                .Include(t => t.Learner)
                .FirstOrDefaultAsync(m => m.AssessmentId == id);
            if (takenassessment == null)
            {
                return NotFound();
            }

            return View(takenassessment);
        }

        // POST: Takenassessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var takenassessment = await _context.Takenassessments.FindAsync(id);
            if (takenassessment != null)
            {
                _context.Takenassessments.Remove(takenassessment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TakenassessmentExists(int id)
        {
            return _context.Takenassessments.Any(e => e.AssessmentId == id);
        }
        public async Task<IActionResult> AssessmentScores(int id)
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); // Return 401 if user is not authenticated
            }

            // Retrieve the current user's ID from the claims
            var currentUserId = int.Parse(User.FindFirst("Id")?.Value ?? "0");

            // Fetch the current user and ensure the Learner relationship is included
            var user = await _context.Users
                .Include(u => u.Learner)
                .FirstOrDefaultAsync(u => u.Id == currentUserId);

            if (user?.Learner == null)
            {
                return NotFound(); // If no learner associated with the user, return 404
            }

            // Query assessment scores for the specific learner
            var scores = await _context.Takenassessments
                .Include(t => t.Assessment) // Ensure Assessment is loaded
                .Where(t => t.LearnerId == user.Learner.LearnerId) // Filter by the learner's ID
                .Select(t => new
                {
                    Title = t.Assessment.Title, // Access the correct Title property
                    Score = t.ScoredPoint,
                    TotalMarks = t.Assessment.TotalMarks
                })
                .ToListAsync();

            return View(scores); // Pass scores to the view
        }


    }
}

