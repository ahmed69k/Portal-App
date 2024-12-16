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
    public class PersonalizationProfilesController : Controller
    {
        private readonly Fm2Context _context;

        public PersonalizationProfilesController(Fm2Context context)
        {
            _context = context;
        }

        // GET: PersonalizationProfiles
        public async Task<IActionResult> Index()
        {
            var fm2Context = _context.PersonalizationProfiles.Include(p => p.Learner);
            return View(await fm2Context.ToListAsync());
        }

        // GET: PersonalizationProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizationProfile = await _context.PersonalizationProfiles
                .Include(p => p.Learner)
                .FirstOrDefaultAsync(m => m.LearnerId == id);
            if (personalizationProfile == null)
            {
                return NotFound();
            }

            return View(personalizationProfile);
        }

        // GET: PersonalizationProfiles/Create
        public IActionResult Create()
        {
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId");
            return View();
        }

        // POST: PersonalizationProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LearnerId,ProfileId,PreferedContentType,EmotionalState,PersonalityType")] PersonalizationProfile personalizationProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalizationProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", personalizationProfile.LearnerId);
            return View(personalizationProfile);
        }

        // GET: PersonalizationProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizationProfile = await _context.PersonalizationProfiles.FindAsync(id);
            if (personalizationProfile == null)
            {
                return NotFound();
            }
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", personalizationProfile.LearnerId);
            return View(personalizationProfile);
        }

        // POST: PersonalizationProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LearnerId,ProfileId,PreferedContentType,EmotionalState,PersonalityType")] PersonalizationProfile personalizationProfile)
        {
            if (id != personalizationProfile.LearnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personalizationProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalizationProfileExists(personalizationProfile.LearnerId))
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
            ViewData["LearnerId"] = new SelectList(_context.Learners, "LearnerId", "LearnerId", personalizationProfile.LearnerId);
            return View(personalizationProfile);
        }

        // GET: PersonalizationProfiles/Delete/5
        // GET: PersonalizationProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personalizationProfile = await _context.PersonalizationProfiles
                .Include(p => p.Learner)
                .FirstOrDefaultAsync(m => m.LearnerId == id);
            if (personalizationProfile == null)
            {
                return NotFound();
            }

            return View(personalizationProfile);
        }

        // POST: PersonalizationProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? learnerId)
        {

            var currentUserId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Fetch the current learner associated with the logged-in user
            var user = await _context.Users.Include(u => u.Learner).FirstOrDefaultAsync(u => u.Id == currentUserId);

            if (user == null || user.Learner == null || user.Role != "Learner")
            {
                return Forbid(); // Only learners should access this page
            }
            // Retrieve the Learner object by learnerId
            Learner learner = await _context.Learners
                .Include(l => l.PersonalizationProfiles)
                .FirstOrDefaultAsync(l => l.LearnerId == learnerId);

            // Check if learner exists and has associated personalization profiles
            if (learner == null || learner.PersonalizationProfiles == null || !learner.PersonalizationProfiles.Any())
            {
                return NotFound();
            }

            // Get the first profileId (or handle other logic if needed)
            var profileId = learner.PersonalizationProfiles.FirstOrDefault()?.ProfileId;

            if (profileId == null)
            {
                return NotFound();
            }

            // Now find the PersonalizationProfile using both learnerId and profileId
            var personalizationProfile = await _context.PersonalizationProfiles
                .FindAsync(learnerId, profileId);

            // If the profile is found, remove it
            if (personalizationProfile != null)
            {
                _context.PersonalizationProfiles.Remove(personalizationProfile);
                await _context.SaveChangesAsync();
            }
            if (user.Role == "Learner")
            {
               return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Learners");

        }


        private bool PersonalizationProfileExists(int id)
        {
            return _context.PersonalizationProfiles.Any(e => e.LearnerId == id);
        }
    }
}
