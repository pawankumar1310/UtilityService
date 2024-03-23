using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Service;
using Structure;
using Models;
namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesofStateController:Controller
    {
        public GetCityStateService _getCitystateService;

        public CitiesofStateController(ICityState citystate)
        {
            _getCitystateService=new GetCityStateService(citystate);
        }

    [HttpPost("GetCitiesBystateId")]
    public async Task<ActionResult<List<CityModel>>> GetCitiesByCountryId([FromBody] List<string> stateIDs)
    {
        try
        {
            List<CityModel> allCities = new List<CityModel>();

            foreach (var stateId in stateIDs)
            {
                var cities = await _getCitystateService.CityofStateService(stateId);
                allCities.AddRange(cities);
            }

            return Ok(allCities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    }
}