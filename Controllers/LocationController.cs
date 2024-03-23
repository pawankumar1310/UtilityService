using DBService;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationDataAccess _dataAccess;

        public LocationController(LocationDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet("GetLocationInfo/{zipCodeID}")]
        public async Task<IActionResult> GetLocationInfo(string zipCodeID)
        {
            var locationInfoList = await _dataAccess.GetLocationInfoFromZipCodeAsync(zipCodeID);

            if (locationInfoList.Count > 0)
            {
                return Ok(locationInfoList);
            }

            return NotFound(); // Return 404 if no data is found
        }
    }
}
