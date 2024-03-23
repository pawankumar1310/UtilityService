using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Structure;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPhoneCodeFromID : ControllerBase
    {

        private readonly GetPhoneCodeService _getPhoneCodeService;
        public GetPhoneCodeFromID(IGetPhoneCodeFromID getPhoneCodeFromID)
        {
            _getPhoneCodeService = new GetPhoneCodeService(getPhoneCodeFromID);
        }
        [HttpGet("{countryID}")]
        public async Task<IActionResult> GetPhoneCode(string countryID)
        {
            try
            {
                var result = await _getPhoneCodeService.PhoneCodeService(countryID);
                if (result != null)
                {
                    return Ok(result.PhoneCode);
                }
                else
                {
                    return NotFound($"CountryID {countryID} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
