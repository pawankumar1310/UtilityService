// CityController.cs
using Service;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace Controllers{

[Route("api/[controller]")]
[ApiController]
public class LocationInfoController : ControllerBase
{
    private readonly LocationInfoService _locationInfoService;

    public LocationInfoController(LocationInfoService locationInfoService)
    {
        _locationInfoService = locationInfoService;
    }

 // ---------------------------------------- GET ALL CITIES -----------------------------------------------


    [HttpGet("GetAllCitiesWithCityID")]
    public async Task<ActionResult<List<City>>> GetAllCities()
    {
        try
        {
            var cities = await _locationInfoService.GetAllCitiesAsync();
            return Ok(cities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


// ------------------------------------- GET COUNTRY, STATE, CITY, AREA BY ZIPCODE ------------------------------------------------


    [HttpGet("GetLocationInfo/{zipCode}")]
    public async Task<ActionResult<LocationInfo>> GetLocationInfoByZipCode(string zipCode)
    {
        try
        {
            var locationInfo = await _locationInfoService.GetLocationInfoByZipCodeAsync(zipCode);

            if (locationInfo != null)
            {
                return Ok(locationInfo);
            }
            else
            {
                return NotFound($"No location information found for zip code {zipCode}");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


// ---------------------------------------- GET STATE, COUNTRY BY CITY -------------------------------------


    [HttpGet("GetStateAndCountryByCityName/{cityID}")]
    public async Task<ActionResult<StateCountry>> GetStateAndCountryByCityID(Guid cityID)
    {
        try
        {
            var locationInfo = await _locationInfoService.GetStateAndCountryByCityIdAsync(cityID);

            if (locationInfo != null)
            {
                return Ok(locationInfo);
            }
            else
            {
                return NotFound($"No location information found for the provided city name");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


// ------------------------------------------- GET COUNTRY BY STATE --------------------------------------------


     [HttpGet("GetCountryByStateName/{stateName}")]
    public async Task<ActionResult<StateCountry>> GetCountryByStateName(string stateName)
    {
        try
        {
            var countryStateInfo = await _locationInfoService.GetCountryByStateNameAsync(stateName);

            if (countryStateInfo != null)
            {
                return Ok(countryStateInfo.Country);
            }
            else
            {
                return NotFound($"No country information found for state {stateName}");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


// ------------------------------------------- GET ZIP CODES BY CITY NAME ------------------------------------------------


    [HttpPost("GetZipCodesByCityNames")]
    public async Task<ActionResult<List<ZipCodes>>> GetZipCodesByCityNames([FromBody] List<string> cityNames)
    {
        try
        {
            var zipCodes = await _locationInfoService.GetZipCodesByCityNamesAsync(cityNames);

            if (zipCodes != null && zipCodes.Count > 0)
            {
                return Ok(zipCodes);
            }
            else
            {
                return NotFound($"No zip code information found for the provided city names");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


//------------------------------------------------ GET AREA BY ZIPCODE --------------------------------------------------

    [HttpGet("GetAreaByZipCode")]
    public async Task<ActionResult<LocationInfo>> GetAreaByZipCode(string zipcode)
    {
        try
        {
            var area = await _locationInfoService.GetAreaByZipCodeAsync(zipcode);
            if(area != null)
            {
                return Ok(area.AreaName);
            }
            else
            {
                return NotFound($"No area found for the provides ZipCode");
            }
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
}
