using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Service;
using Structure;
using Models;
using Automation;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController:Controller
    {
        public  GetCountryNameService _getCountryNameService;
        public CountryController(ICountryName countryName)
        {
            _getCountryNameService=new GetCountryNameService(countryName);
        }
        [HttpGet]
        public async Task<ActionResult<List<CountryAndIDModel>>> GetAllCountries()
        {
            try
            {
                var countries = await _getCountryNameService.GetCountryService();
                return Ok(countries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

}