using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Models;
using Service;
using Structure;
using DTO;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceInformationController:Controller
    {
        public readonly GetPlaceInfoService _placeInfoService;
        public PlaceInformationController(IPlaceInformation placeInformation)
        {
            _placeInfoService=new GetPlaceInfoService(placeInformation);
        }

        [HttpGet("{zipCodeID}")]
        public async Task<ActionResult<List<PlaceInformationModel>>> GetPlaceInformationByZipCode(string zipCodeID)
        {
            var placeInformationList = await _placeInfoService.GetPlaceInformationService(zipCodeID);

            if (placeInformationList == null || placeInformationList.Count == 0)
            {
                return NotFound();
            }

            return placeInformationList;
        }

        [HttpGet("GetLocationInfo/{zipCodeID}")]
        public async Task<IActionResult> GetLocationInfo(string zipCodeID)
        {
            List<PlaceModel> locationInfo = await _placeInfoService.GetPlacesService(zipCodeID);

            if (locationInfo != null)
            {
                return Ok(locationInfo);
            }

            return NotFound(); // Return 404 if no data is found
        }

    }
    
    
}