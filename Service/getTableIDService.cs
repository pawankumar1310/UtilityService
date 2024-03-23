
using Structure;

namespace Service
{
    public class getTableIDService
    {
        
        private readonly ITable iTable;

        public getTableIDService(ITable iTable)
        {
            this.iTable=iTable;
           
        }
        public async Task<string> GetTableIDAbs(string tableName)
        {
            string result=await iTable.IsValidName(tableName);
            return result;
        }
        public async Task<string> GetDatabaseID(string tableName)
        {
            string result=await iTable.IsValidNameDb(tableName);
            return result;
        }
    }
}