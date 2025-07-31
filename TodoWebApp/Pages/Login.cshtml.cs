// File: Pages/Login.cshtml.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TodoWebApp.Data;
using TodoWebApp.Models;
using TodoWebApp.Services;

namespace TodoWebApp.Pages // ?? CORRECTED NAMESPACE
{
    public class LoginModel : PageModel
    {
        private readonly MovieAppContext _context;
        private readonly JwtService _jwt;

        public LoginModel(MovieAppContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [BindProperty]
        public UserLogin Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == Input.Username);

            if (user == null || !VerifyPassword(Input.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            var token = _jwt.GenerateToken(user.Username);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return LocalRedirect("/");
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }
}