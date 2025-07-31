using Microsoft.AspNetCore.Mvc;
using System.Text;
using TodoWebApp.Data;
using TodoWebApp.Models;
using TodoWebApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace TodoWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MovieAppContext _context;
        private readonly JwtService _jwt;

        public AuthController(MovieAppContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserSignup model)
        {
            if (_context.Users.Any(u => u.Username == model.Username))
                return BadRequest("User already exists");

            var hashed = HashPassword(model.Password);
            var user = new User { Username = model.Username, Email = model.Email, PasswordHash = hashed };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _jwt.GenerateToken(user.Username);

            //Response.Cookies.Append("jwt", token, new CookieOptions { HttpOnly = true, Secure = true });

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1),
                Path = "/"
            });

            return Ok(new { message = "Login successful" });
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
