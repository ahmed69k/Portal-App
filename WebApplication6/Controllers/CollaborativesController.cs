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


        [HttpGet]
        public IActionResult SetDeadline(int questId)
        {
            // Pass the quest ID to the view
            return View(new Collaborative { QuestId = questId });
        }

        [HttpPost]
        public async Task<IActionResult> SetDeadline(int questId, DateTime deadline)
        {
            // Validate the deadline date
            if (deadline < new DateTime(1753, 1, 1) || deadline > new DateTime(9999, 12, 31))
            {
                ModelState.AddModelError("Deadline", "The deadline must be between 1/1/1753 and 12/31/9999.");
                return View();
            }

            // Call the stored procedure if the model state is valid
            if (ModelState.IsValid)
            {
                try
                {
                    // Call the stored procedure with parameters
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC DeadlineUpdate @QuestID, @deadline",
                        new SqlParameter("@QuestID", questId),
                        new SqlParameter("@deadline", deadline)
                    );
                    // Redirect to the Quests page
                    return RedirectToAction("Index", "Quests");
                }
                catch (Exception ex)
                {
                    // Handle exceptions here, e.g., log the error or show a user-friendly message
                    ModelState.AddModelError("", "An error occurred while setting the deadline.");
                    return View();
                }
            }

            // If something goes wrong, return the view again with validation errors
            return View();
        }






    }
}
