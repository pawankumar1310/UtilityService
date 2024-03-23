using DBService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Service;
using Structure;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryCurrency : ControllerBase
    {
        private readonly CurrencyServices _currencyServices;
        public CountryCurrency(CurrencyServices currencyServices)
        {
            _currencyServices=currencyServices;
        }

        [HttpGet("GetCurrencyDetails")]
        public async Task<IActionResult> GetCurrencyDeatails()
        {
            try
            {
                var data=await _currencyServices.GetListOfCurrencyList();
                return Ok(data);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
