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
    public class StateController:Controller
    {
        public getStateNameService _getStateNameService;

        public StateController(IStateNames stateNames)
        {
            _getStateNameService=new getStateNameService(stateNames);
        }

        [HttpPost]
        public async Task<ActionResult<List<StateModel>>> GetStatesByCountryIds([FromBody] List<string> countryIds)
        {
            try
            {
                List<StateModel> allStates = new List<StateModel>();

                foreach (var countryId in countryIds)
                {
                    var states = await _getStateNameService.StateNameService(countryId);
                    allStates.AddRange(states);
                }

        return Ok(allStates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}