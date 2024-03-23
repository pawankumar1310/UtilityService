using Microsoft.AspNetCore.Mvc;
using Structure;
using Service;

namespace Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class SaltController : ControllerBase
    {
        private readonly ISalt saltGenerator;

        public SaltController(ISalt saltGenerator)
        {
            this.saltGenerator = saltGenerator;
        }

        [HttpGet]
        public IActionResult GetSalt()
        {
            try
            {
                // Generate a random salt using the injected service
                byte[] saltBytes = saltGenerator.GenerateRandomSaltService();

                // Convert the byte array to a string for representation
                string salt = Convert.ToBase64String(saltBytes);

                return Ok(new { Salt = salt });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
