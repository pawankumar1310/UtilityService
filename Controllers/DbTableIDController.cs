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
    public class DbTableIDController:Controller
    {
        public  getTableNameService _getTableNameService;
        public DbTableIDController(ITableName iTableName)
        {
            _getTableNameService=new getTableNameService(iTableName);
        }

        [HttpGet("GetDatabaseAndTableNameList")]
        public async Task<IActionResult> GetDatabaseAndTableNameList(string tableID)
        {
            try
            {
                List<string> resultList = await _getTableNameService.GetTableName(tableID);

                if (resultList.Count > 0)
                {
                    return Ok(resultList);
                }
                else
                {
                    return NotFound("No database and table found for the specified table ID.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        
    }


}
