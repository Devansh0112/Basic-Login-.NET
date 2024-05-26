using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] User model)
        {
            // For simplicity, we're using hardcoded username and password.
            // In a real application, you should check these against a database.
            if (model.Username == "user" && model.Password == "password")
            {
                return Ok(new { success = true });
            }
            return Unauthorized(new { success = false });
        }
    }
}
