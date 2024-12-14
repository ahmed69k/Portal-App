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
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

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
        public async Task<IActionResult> Details(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Users");
            }

            var currentUserId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Include the Learner details in the query to avoid lazy loading issues
            var user = await _context.Users
                .Include(u => u.Learner)  // Eagerly load the Learner details
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Authorization logic:
            // - Learners can only access their own profiles
            // - Admins can access any profile
            // - Other users (e.g., instructors) can have custom logic if needed
            if (currentUserRole != "Admin" && currentUserId != user.Id)
            {
                return Forbid(); // Deny access with HTTP 403
            }

            return user.Role switch
            {
                "Admin" => View("AdminDetails", user),
                "Instructor" => View("InstructorDetails", user),
                "Learner" => View("LearnerDetails", user),
                _ => View("Details", user) // Fallback for undefined roles
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
        public async Task<IActionResult> Register([Bind("Email,Name,Role,PasswordHash,ProfilePicture")] User user, IFormFile? profilePicture, int? learnerId, int? instructorId)
        {
            if (ModelState.IsValid)
            {
                // Ensure the upload path exists
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Process the profile picture only if it is provided
                if (profilePicture != null && profilePicture.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePicture.FileName); // Use a unique name to avoid conflicts
                    string filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await profilePicture.CopyToAsync(stream);
                    }
                    user.ProfilePicture = "/uploads/" + fileName;
                }
                else
                {
                    // Set a default profile picture or leave it null
                    user.ProfilePicture = ""; // Replace with an actual default image if needed
                }

                // Assign LearnerId or InstructorId based on the role
                if (user.Role == "Learner" && learnerId.HasValue)
                {
                    user.LearnerId = learnerId;
                    user.InstructorId = null; // Ensure the InstructorId is null
                }
                else if (user.Role == "Instructor" && instructorId.HasValue)
                {
                    user.InstructorId = instructorId;
                    user.LearnerId = null; // Ensure the LearnerId is null
                }


                // Add the new user to the database
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = user.Id });
            }

            return View(user);
        }








        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

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

            // Create user claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingUser.Name),
                new Claim(ClaimTypes.Email, existingUser.Email),
                new Claim("Id", existingUser.Id.ToString()), // Add user ID as a claim
                new Claim(ClaimTypes.Role, existingUser.Role)
            };



            // Create claims identity and principal
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Persistent login
                ExpiresUtc = DateTime.UtcNow.AddDays(7) // Cookie expiration
            };

            // Sign in user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Details", new { id = existingUser.Id });
        }

        // POST: Users/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Users");
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Add your password hash verification logic here (e.g., BCrypt or Identity)
            return enteredPassword == storedPasswordHash; // Replace this with hashing logic
        }

        // Helper method to check if the current user is an admin
        private bool UserIsAdmin()
        {
            return User.Identity.IsAuthenticated && User.IsInRole("Admin");
        }

        // Remaining actions (Create/Edit/Delete)...

        [HttpGet]
        [Route("Users/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            // Ensure the user role is Learner
            if (user.Role != "Learner")
            {
                TempData["ErrorMessage"] = "Unauthorized access. Only learner accounts can be deleted.";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePersonalization(int id)
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Users");
            }

            // Get the user ID from the logged-in user's claims
            var currentUserId = int.Parse(User.FindFirst("Id")?.Value ?? "0");
            if (id != currentUserId)
            {
                return Forbid(); // Ensure the user can only remove their own personalization
            }

            // Find the user in the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            // Clear the personalization (e.g., remove profile picture)
            user.ProfilePicture = null;

            // Save the changes to the database
            _context.Update(user);
            await _context.SaveChangesAsync();

            // Set the success message in ViewData
            ViewData["SuccessMessage"] = "Your personalization settings have been successfully removed.";

            // Return the updated details view for the user
            return View("LearnerDetails", user);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Index");
            }

            if (user.Role != "Learner")
            {
                TempData["ErrorMessage"] = "Unauthorized access.";
                return RedirectToAction("Index");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Learner account successfully deleted.";
            return RedirectToAction("Index");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }


}
