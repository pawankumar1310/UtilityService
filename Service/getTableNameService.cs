using Structure;
namespace Service
{
    public class getTableNameService
    {
        private readonly ITableName iTableName;

        public getTableNameService(ITableName iTableName)
        {
            this.iTableName=iTableName;
           
        }
        public async Task<List<string>> GetTableName(string tableID)
        {
            
            List<string> result=await iTableName.IsValidTableName(tableID);
            return result;
        }


    }
}