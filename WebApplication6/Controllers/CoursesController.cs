using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

public class CoursesController : Controller
{
    private readonly Fm2Context _context;

    public CoursesController(Fm2Context context)
    {
        _context = context;
    }

    // GET: Courses
    public async Task<IActionResult> Index()
    {
        return View(await _context.Courses.ToListAsync());
    }

    // GET: Courses/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Include the related Modules when fetching the course
        var course = await _context.Courses
            .Include(c => c.Modules) // Eager loading Modules
            .FirstOrDefaultAsync(m => m.CourseId == id);

        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }


    // Add this action in the CoursesController
    public async Task<IActionResult> MyCourses()
    {
        var courses = await _context.Courses.ToListAsync();
        return View(courses);
    }

    // GET: Courses/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Courses/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CourseId,Title,LearningObjective,CreditPoints,DifficultyLevel,Description")] Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(course);
    }

    // GET: Courses/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return View(course);
    }

    // POST: Courses/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CourseId,Title,LearningObjective,CreditPoints,DifficultyLevel,Description")] Course course)
    {
        if (id != course.CourseId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(course);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(course.CourseId))
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
        return View(course);
    }

    // GET: Courses/Delete/5
    // GET: Courses/Delete/5
[Authorize(Roles = "Instructor")] // Only instructors can access this action
                                  // GET: Courses/Delete/5
    [Authorize(Roles = "Instructor")] // Only instructors can access this action
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _context.Courses
            .Include(c => c.CourseEnrollments)
            .FirstOrDefaultAsync(m => m.CourseId == id);

        if (course == null)
        {
            return NotFound();
        }

        // Check if the course has any enrollments
        if (course.CourseEnrollments.Any())
        {
            ViewBag.ErrorMessage = "This course cannot be deleted because students are currently enrolled.";
            return View("Error");
        }

        return View(course); // This will render DeleteConfirmation.cshtml
    }

    // POST: Courses/Delete/5
    [Authorize(Roles = "Instructor")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await _context.Courses
            .Include(c => c.CourseEnrollments)
            .FirstOrDefaultAsync(c => c.CourseId == id);

        if (course != null)
        {
            // Ensure no students are enrolled before deletion
            if (!course.CourseEnrollments.Any())
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ErrorMessage = "This course cannot be deleted because students are currently enrolled.";
            return View("Error");
        }

        return NotFound();
    }


    private bool CourseExists(int id)
    {
        return _context.Courses.Any(e => e.CourseId == id);
    }
    public async Task<IActionResult> PreviousCourses(int userId)
    {
        var completedCourses = await _context.CourseEnrollments
            .Where(e => e.LearnerId == userId && e.Course.Prereqs.Any()) // Ensure Prereqs is not empty
            .Include(e => e.Course)
            .Select(e => e.Course)
            .ToListAsync();

        if (!completedCourses.Any())
        {
            ViewBag.Message = "No previously taken courses found.";
        }

        return View(completedCourses);
    }
    public async Task<IActionResult> CheckPrerequisites(int userId, int courseId)
    {
        // Fetch the course and its prerequisites
        var course = await _context.Courses
            .Include(c => c.Prereqs) // Assuming Prereqs is a collection of prerequisite courses
            .FirstOrDefaultAsync(c => c.CourseId == courseId);

        if (course == null)
        {
            return NotFound("Course not found.");
        }

        // Fetch the user's completed courses
        var completedCourses = await _context.CourseEnrollments
            .Where(e => e.LearnerId == userId && e.CompletionDate.HasValue) // Ensure course is completed
            .Select(e => e.CourseId)
            .ToListAsync();

        // Check if all prerequisites are completed
        var prerequisitesCompleted = course.Prereqs.All(prereq => completedCourses.Contains(prereq.CourseId));

        // Pass the result to the view
        ViewBag.PrerequisitesCompleted = prerequisitesCompleted;
        ViewBag.CourseTitle = course.Title;
        return View(course);
    }




    // ModulesController nested inside CoursesController
    public class ModulesController : Controller
    {
        private readonly Fm2Context _context;

        public ModulesController(Fm2Context context)
        {
            _context = context;
        }

        // GET: Modules/ByCourse/5
        public async Task<IActionResult> ByCourse(int? courseId)
        {
            if (courseId == null)
            {
                return NotFound("Course ID is required.");
            }

            // Fetch modules linked to the specified CourseId
            var modules = await _context.Modules
                .Include(m => m.Course)
                .Where(m => m.CourseId == courseId)
                .ToListAsync();

            if (modules == null || !modules.Any())
            {
                ViewBag.Message = "No modules found for the selected course.";
                return View(new List<Module>());
            }

            ViewBag.CourseTitle = modules.FirstOrDefault()?.Course?.Title ?? "Selected Course";
            return View(modules);
        }
    }
}
