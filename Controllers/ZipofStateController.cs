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
    public class ZipofStateController:Controller
    {
        public getZipofStateService _getZipofStateService;

        public ZipofStateController(IZipOfState izipofState)
        {
            _getZipofStateService=new getZipofStateService(izipofState);
        }

    [HttpPost("GetZipBystateId")]
    public async Task<ActionResult<List<ZipCodeModel>>> GetZipByStateId([FromBody] List<string> stateIDs)
    {
        try
        {
            List<ZipCodeModel> allZip = new List<ZipCodeModel>();

            foreach (var zipcode in stateIDs)
            {
                var zip = await _getZipofStateService.ZipOfState(zipcode);
                allZip.AddRange(zip);
            }

            return Ok(allZip);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    }
}