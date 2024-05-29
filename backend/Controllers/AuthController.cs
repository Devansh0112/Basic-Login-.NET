using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Services;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var existingUser = await _userService.GetByUsernameAsync(model.Username);
            if (existingUser != null && BCrypt.Net.BCrypt.Verify(model.Password, existingUser.Password))
            {
                return Ok(new { success = true, message = "Login Successful" });
            }

            return Unauthorized(new { success = false, message = "Login Failed" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User model)
        {
            var existingUser = await _userService.GetByUsernameAsync(model.Username);
            if (existingUser != null)
            {
                return BadRequest(new { success = false, message = "User already exists" });
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var newUser = new User { Username = model.Username, Password = hashedPassword };
            await _userService.CreateAsync(newUser);

            return Ok(new { success = true });
        }
    }
}
