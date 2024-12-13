using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authentication;

namespace WebApplication6.Controllers
{
    public class UsersController : Controller
    {
        private readonly Fm2Context _context;

        public UsersController(Fm2Context context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return user.Role switch
            {
                "Admin" => View("AdminDetails", user),
                "Instructor" => View("InstructorDetails", user),
                "Learner" => View("LearnerDetails", user),
                _ => View("Details", user) // Fallback
            };
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Email,Name,Role,PasswordHash,ProfilePicture")] User user, IFormFile profilePicture)
        {
            if (ModelState.IsValid)
            {
                // Ensure the upload path exists
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                if (profilePicture != null)
                {
                    string filePath = Path.Combine(uploadPath, Path.GetFileName(profilePicture.FileName));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await profilePicture.CopyToAsync(stream);
                    }
                    user.ProfilePicture = "/uploads/" + Path.GetFileName(profilePicture.FileName);
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = user.Id });
            }
            return View(user);
        }

        // GET: Users/Login
        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
            {
                ViewBag.Error = "Email and password are required.";
                return View(user);
            }

            // Verify credentials
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser == null || !VerifyPassword(user.PasswordHash, existingUser.PasswordHash))
            {
                ViewBag.Error = "Invalid credentials. Please try again.";
                return View(user);
            }

            // Set up session or cookies
            // Example: Use ClaimsPrincipal to log in the user (if not using Identity)
            // await SignInAsync(existingUser);

            return RedirectToAction("Details", new { id = existingUser.Id });
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Add your password hash verification logic here (e.g., BCrypt or Identity)
            return enteredPassword == storedPasswordHash; // Replace this with hashing logic
        }


        // POST: Users/RemoveInstructor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveInstructor(int id)
        {
            if (!UserIsAdmin())
            {
                return Forbid();
            }

            var instructor = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.Role == "Instructor");
            if (instructor == null)
            {
                return NotFound();
            }

            _context.Users.Remove(instructor);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Instructor's account has been permanently removed.";
            return RedirectToAction(nameof(Index));
        }


        // POST: Users/RemovePersonalization
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePersonalization(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.Role != "Learner" && !UserIsAdmin())
            {
                return Forbid(); // Ensure only learners and admins can perform this action
            }

            // Clear personalization settings
            user.ProfilePicture = null; // Example: Remove profile picture
            TempData["Message"] = "Personalization settings have been removed.";
            return RedirectToAction(nameof(Details), new { id = user.Id });
            // Add other customization settings to reset as needed
            // e.g., user.LearningGoals = null;

            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = user.Id });
        }

        // Helper method to check if the current user is an admin
        private bool UserIsAdmin()
        {
            // Replace with your actual logic to check the role of the current logged-in user
            return User.Identity.IsAuthenticated && User.IsInRole("Admin");
        }




        // Remaining actions (Create/Edit/Delete)...
        [HttpGet]
        [Route("Users/Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("LearnerDetails", new { id });
            }

            // Ensure only the learner themselves can access this
            if (User.Identity.Name != user.Email || user.Role != "Learner")
            {
                TempData["ErrorMessage"] = "Unauthorized access.";
                return RedirectToAction("LearnerDetails", new { id });
            }

            return View(user); // Render the Delete confirmation view
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Users/Delete/{id:int}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("LearnerDetails", new { id });
            }

            if (User.Identity.Name != user.Email || user.Role != "Learner")
            {
                TempData["ErrorMessage"] = "Unauthorized access.";
                return RedirectToAction("LearnerDetails", new { id });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your account has been successfully deleted.";
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }



        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
