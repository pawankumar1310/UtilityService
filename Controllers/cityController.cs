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
    public class CityController:Controller
    {
        public GetCityNameService _getCityNameService;

        public CityController(ICityNames cityNames)
        {
            _getCityNameService=new GetCityNameService(cityNames);
        }

        [HttpPost("GetCitiesByCountryId")]
    public async Task<ActionResult<List<CityModel>>> GetCitiesByCountryId([FromBody] List<string> countryIds)
    {
        try
        {
            List<CityModel> allCities = new List<CityModel>();

            foreach (var countryId in countryIds)
            {
                var cities = await _getCityNameService.CityNameService(countryId);
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