using DTO.UtilityService;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ZipCodeController : ControllerBase
    {
        [HttpPost("GetLocationInfoByZipCodeID")]
        public IActionResult GetLocationInfoByZipCodeID(ZipCodeIDRequest zipCodeIDModelRequest)
        {
            if (!string.IsNullOrEmpty(zipCodeIDModelRequest.ZipCodeID))
            {
                try
                {
                    ZipCodeService zipCodeService = new();
                    return Ok(zipCodeService.GetPlaceInfoByZipCodeID(zipCodeIDModelRequest));
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("GetZipCodeIDByZipCode")]
        public IActionResult GetZipCodeIDByZipCode(ZipCodeRequest zipCodeRequest)
        {
            if (!string.IsNullOrEmpty(zipCodeRequest.ZipCode))
            {
                try
                {
                    ZipCodeService zipCodeService = new();
                    return Ok(zipCodeService.GetZipCodeIDByZipCode(zipCodeRequest));
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }

}