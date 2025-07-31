using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Pages
{
    public class SignupModel : PageModel
    {
        private readonly MovieAppContext _context;

        public SignupModel(MovieAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserSignup Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if user already exists
            if (await _context.Users.AnyAsync(u => u.Username == Input.Username))
            {
                ModelState.AddModelError("Input.Username", "Username is already taken.");
                return Page();
            }

            // Hash the password and create the user
            var hashedPassword = HashPassword(Input.Password);
            var user = new User
            {
                Username = Input.Username,
                Email = Input.Email,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Redirect to the login page after successful registration
            return RedirectToPage("/login");
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}