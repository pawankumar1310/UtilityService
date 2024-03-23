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
    public class InternalTableController : Controller
    {
        public  getTableIDService _getTableIDService;
        public InternalTableController(ITable iTable)
        {
            _getTableIDService=new getTableIDService(iTable);
        }
        [HttpGet]
        public async Task<IActionResult> getID(string tableName)
        {
            string  tableIDResult = await _getTableIDService.GetTableIDAbs(tableName);
            string databaseIDResult=await _getTableIDService.GetDatabaseID(tableName);
            ReferenceModel referenceModel=new ReferenceModel();
            referenceModel.TableID=tableIDResult;
            referenceModel.DatabaseID=databaseIDResult;
            
        
            if(tableIDResult!=null)
            {
               
               return Ok($"Table ID :{referenceModel.TableID} and Database ID is {referenceModel.DatabaseID}");
            }
            else
            {
                return BadRequest("No table Found");
              
            }
        }

    }

}