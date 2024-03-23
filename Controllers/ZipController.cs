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
    public class ZipController:ControllerBase
    {
        public getZipCodeService _getZipCodeService;

        public ZipController(IZipCodeNames zipNames)
        {
            _getZipCodeService=new getZipCodeService(zipNames);
        }

    [HttpPost("GetZipByCountryId")]
    public async Task<ActionResult<List<ZipCodeModel>>> GetCitiesByCountryId([FromBody] List<string> countryIds)
    {
        try
        {
            List<ZipCodeModel> allCities = new List<ZipCodeModel>();

            foreach (var countryId in countryIds)
            {
                var cities = await _getZipCodeService.ZipService(countryId);
                allCities.AddRange(cities);
            }

            return Ok(allCities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpGet("{zipCode}")]
    public async Task<IActionResult> GetZipCodeId(long zipCode)
    {
      var zipCodeId =await _getZipCodeService.GetZipCodeID(zipCode);

          if (zipCodeId != null)
          {
              return Ok(zipCodeId);
          }

            return NotFound();
    }

        
    [HttpGet("getZipcode/{zipCodeID}")]
    public async Task<IActionResult> GetZipCodeByZipCodeID(string zipCodeID)
    {
      var zipCode =await _getZipCodeService.GetZipCodeByZipCodeID(zipCodeID);

          if (zipCode != null)
          {
              return Ok(zipCode);
          }

            return NotFound();
    }




    }
}