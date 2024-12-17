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
    public class LearningActivitiesController : Controller
    {
        private readonly Fm2Context _context;

        public LearningActivitiesController(Fm2Context context)
        {
            _context = context;
        }

        // GET: LearningActivities
        public async Task<IActionResult> Index()
        {
            var fm2Context = _context.LearningActivities.Include(l => l.Module);
            return View(await fm2Context.ToListAsync());
        }

        // GET: LearningActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningActivity = await _context.LearningActivities
                .Include(l => l.Module)
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (learningActivity == null)
            {
                return NotFound();
            }

            return View(learningActivity);
        }

        // GET: LearningActivities/Create
        public IActionResult Create()
        {
            // Fetch Modules that have Courses
            var modulesWithCourses = _context.Modules
                .Include(m => m.Course)
                .Where(m => m.Course != null) // Ensure only Modules with Courses are included
                .Select(m => new
                {
                    ModuleId = m.ModuleId,
                    DisplayText = $"Module: {m.Title} - Course: {m.Course.Title}"
                })
                .ToList();

            // Populate the SelectList safely
            ViewData["ModuleId"] = new SelectList(modulesWithCourses, "ModuleId", "DisplayText");

            return View();
        }


        // POST: LearningActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivityId,ModuleId,ActivityType,InstructionDetails,MaxPoints")] LearningActivity learningActivity)
        {
            if (ModelState.IsValid)
            {
                // Fetch CourseId based on the selected ModuleId
                var module = await _context.Modules
                    .Include(m => m.Course)
                    .FirstOrDefaultAsync(m => m.ModuleId == learningActivity.ModuleId);

                if (module != null)
                {
                    learningActivity.CourseId = module.Course.CourseId; // Assign the CourseId dynamically
                }

                _context.Add(learningActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate ViewData in case of validation error
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "Title", learningActivity.ModuleId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", learningActivity.CourseId);

            return View(learningActivity);
        }


        // GET: LearningActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningActivity = await _context.LearningActivities.FindAsync(id);
            if (learningActivity == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleId", learningActivity.ModuleId);
            return View(learningActivity);
        }

        // POST: LearningActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityId,ModuleId,CourseId,ActivityType,InstructionDetails,MaxPoints")] LearningActivity learningActivity)
        {
            if (id != learningActivity.ActivityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learningActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearningActivityExists(learningActivity.ActivityId))
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
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleId", learningActivity.ModuleId);
            return View(learningActivity);
        }

        // GET: LearningActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var learningActivity = await _context.LearningActivities
                .Include(l => l.Module)
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (learningActivity == null)
            {
                return NotFound();
            }

            return View(learningActivity);
        }

        // POST: LearningActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var learningActivity = await _context.LearningActivities.FindAsync(id);
            if (learningActivity != null)
            {
                _context.LearningActivities.Remove(learningActivity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearningActivityExists(int id)
        {
            return _context.LearningActivities.Any(e => e.ActivityId == id);
        }


    }
}
