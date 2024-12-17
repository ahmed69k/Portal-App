using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class Takenassessments1Controller : Controller
    {
        private readonly Fm2Context _context;

        public Takenassessments1Controller(Fm2Context context)
        {
            _context = context;
        }

        // GET: Takenassessments1
        public async Task<IActionResult> Index()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(); // Return 401 if user is not authenticated
            }

            // Retrieve the current user's ID from the claims
            

            // Fetch the current user and ensure the Learner relationship is included
;



            // Query assessment scores for the specific learner
            var scores = await _context.Takenassessments
                .Include(t => t.Assessment) // Filter by the learner's ID
                .Select(t => new
                {
                    Title = t.Assessment.Title,
                    AssID = t.AssessmentId,// Access the correct Title property
                    Score = t.ScoredPoint,
                    TotalMarks = t.Assessment.TotalMarks,
                    LearnerId = t.LearnerId,
                    Name = t.Learner.FirstName + " "+t.Learner.LastName
                })
                .ToListAsync();

            return View(scores);
        }

        // GET: Takenassessments1/Details/5
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

        // GET: Takenassessments1/Create
        public IActionResult Create()
        {
            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "Id", "Id");
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId");
            return View();
        }

        // POST: Takenassessments1/Create
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

        [HttpGet]
        public async Task<IActionResult> Edit(int AssID, int learnerId)
        {
            var takenAssessment = await _context.Takenassessments
                .FirstOrDefaultAsync(t => t.AssessmentId == AssID && t.LearnerId == learnerId);

            if (takenAssessment == null)
            {
                return NotFound();
            }

            return View(takenAssessment);
        }



        // POST: Takenassessments1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int AssessmentId, int LearnerId, int ScoredPoint)
        {
            // Ensure the model is valid
            if (ModelState.IsValid)
            {
                try
                {
                    // Execute the GradeUpdate stored procedure
                    var learnerIdParam = new SqlParameter("@LearnerID", LearnerId);
                    var assessmentIdParam = new SqlParameter("@AssessmentID", AssessmentId);
                    var pointsParam = new SqlParameter("@points", ScoredPoint);

                    // Execute the stored procedure
                    await _context.Database.ExecuteSqlRawAsync("EXEC GradeUpdate @LearnerID, @AssessmentID, @points",
                                                                learnerIdParam, assessmentIdParam, pointsParam);

                    // Optionally, you can check the result or handle any exception

                    // Redirect to Index after successful update
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Handle exceptions here (logging, custom error messages, etc.)
                    ModelState.AddModelError("", "An error occurred while updating the grade.");
                }
            }

            // If model state is invalid, return the view with the current values
            return View();
        }


        private bool TakenassessmentExists(int AssessmentId, int LearnerId)
        {
            return _context.Takenassessments.Any(e => e.AssessmentId == AssessmentId && e.LearnerId == LearnerId);
        }


        // GET: Takenassessments1/Delete/5
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

        // POST: Takenassessments1/Delete/5
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
    }
}
