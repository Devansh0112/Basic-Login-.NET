// Controllers/DataController.cs
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        [HttpGet("validation")]
        public IActionResult GetValidationData()
        {
            var data = new
            {
                labels = new[] { "January", "February", "March", "April" },
                datasets = new[]
                {
                new {
                    label = "Errors",
                    data = new[] { 65, 59, 80, 81 },
                    backgroundColor = "rgba(75,192,192,0.4)",
                    borderColor = "rgba(75,192,192,1)"
                }
            }
            };
            return Ok(data);
        }
    }

}