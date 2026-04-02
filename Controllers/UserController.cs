using login_register_react_app.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_registration_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            var email = User.Claims.First(c => c.Type == "email").Value;
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null) return NotFound("User not found");

            return Ok(new { user.Id, user.Name, user.Email, user.CreatedAt });
        }
    }
}
