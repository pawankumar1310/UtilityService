using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Models;
using Service;
using Structure;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneCodeController:Controller
    {
        public PhoneCodeService phoneCodeService;
        public PhoneCodeController(IPhoneCode phoneCode)
        {
            phoneCodeService=new PhoneCodeService(phoneCode);
        }
        [HttpGet]
    public async Task<ActionResult<List<PhoneCodeModel>>> GetCountryData()
    {
        try
        {
            List<PhoneCodeModel> countries = await phoneCodeService.getPhoneCodeService();
            return Ok(countries);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    }

}
