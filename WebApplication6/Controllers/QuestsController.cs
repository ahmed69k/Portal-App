using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class QuestsController : Controller
    {
        private readonly Fm2Context _context;

        public QuestsController(Fm2Context context)
        {
            _context = context;
        }

        // GET: Quests
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quests.ToListAsync());
        }

        // GET: Quests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests
                .FirstOrDefaultAsync(m => m.QuestId == id);
            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // GET: Quests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        // GET: Quests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }
            return View(quest);
        }

        // POST: Quests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestId,DifficultyLevel,Criteria,Description,Title")] Quest quest)
        {
            if (id != quest.QuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestExists(quest.QuestId))
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
            return View(quest);
        }

        // GET: Quests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests
                .FirstOrDefaultAsync(m => m.QuestId == id);
            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // POST: Quests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quest = await _context.Quests.FindAsync(id);
            if (quest != null)
            {
                _context.Quests.Remove(quest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestExists(int id)
        {
            return _context.Quests.Any(e => e.QuestId == id);
        }
        [HttpPost]


        public async Task<IActionResult> Create(int questId, string difficultyLevel, string criteria, string description, string title, int maxNumParticipants, DateTime deadline)
        {
            // Call the stored procedure with parameters
            var result = await _context.Database.ExecuteSqlRawAsync(
                "EXEC CollaborativeQuest @QuestID, @difficulty_level, @criteria, @description, @title, @Maxnumparticipants, @deadline",
                new SqlParameter("@QuestID", questId),
                new SqlParameter("@difficulty_level", difficultyLevel),
                new SqlParameter("@criteria", criteria),
                new SqlParameter("@description", description),
                new SqlParameter("@title", title),
                new SqlParameter("@Maxnumparticipants", maxNumParticipants),
                new SqlParameter("@deadline", deadline));

            return RedirectToAction("Index");
        }
        // GET: Quests/Join/5
        public async Task<IActionResult> Join(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get the current logged-in user's UserName (assuming you're using ASP.NET Core Identity)
            var userName = User.Identity.Name; // Or you can use a different approach to get the logged-in user's username

            if (userName == null)
            {
                return Unauthorized(); // User must be logged in
            }

            // Retrieve the user from the User table using the logged-in user's UserName
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == userName);

            if (user == null)
            {
                return NotFound(); // If the user does not exist
            }

            // Get the LearnerID from the user (not the UserID)
            var learnerId = user.LearnerId; // This is the LearnerID that you want to use

            // Retrieve the Quest based on the QuestID
            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
            {
                return NotFound(); // If the quest does not exist
            }

            // Create a new LearnersCollaboration entry to join the quest
            var collaboration = new LearnersCollaboration
            {
                LearnerId = (int)learnerId, // Use LearnerID from the User
                QuestId = quest.QuestId // Use QuestID
            };

            // Add the collaboration record to the database
            _context.LearnersCollaborations.Add(collaboration);
            await _context.SaveChangesAsync();

            // Redirect to the Quest Index or the Quest Details page
            return RedirectToAction(nameof(Index)); // Or RedirectToAction(nameof(Details), new { id = quest.QuestId });
        }


    }
}
