using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Structure;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private readonly IGenerateOtp _generateOtp;
        public OtpController(IGenerateOtp generateOtp)
        {
            _generateOtp = generateOtp;
        }
        [HttpGet]
        public async Task<IActionResult> GetOtp()
        {
            int result=await _generateOtp.OtpService();
            if(result>0)
            {
                return Ok(result);
            }
            else 
            { 
                return BadRequest(); 
            }


        }
    }
}
