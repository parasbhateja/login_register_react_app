//using BCrypt.Net;
using login_register_react_app.Data;
using login_register_react_app.Models;
using login_register_react_app.Services;

using Microsoft.AspNetCore.Mvc;
//using System.Linq;

namespace login_registration_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest("Email already exists");
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var dbUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (dbUser == null || !BCrypt.Net.BCrypt.Verify(user.PasswordHash, dbUser.PasswordHash))
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _jwt.GenerateToken(dbUser.Id.ToString(), dbUser.Email);
            return Ok(new { token });
        }
    }
}
