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
    public class StateIDController:Controller
    {
        public getStateIdService _getStateIdService;

        public StateIDController(IState state)
        {
            _getStateIdService=new getStateIdService(state);
        }
        [HttpGet]
        public async Task<ActionResult<List<StateModel>>> GetStateIdandName()
        {
            try
            {
                var states=await _getStateIdService.GetStateNameAndIdService();
                return Ok(states); 
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        } 

    }
    
}