using System.Data;
using System.Data.SqlClient;
using Structure;

namespace DBService
{
    public class GetTableName:ITableName
    {
        private readonly IConfiguration _configuration;
        public GetTableName(IConfiguration configuration)
        {
                _configuration=configuration;
        }
        public async Task<List<string>> IsValidTableName(string tableID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("GetDatabaseAndTableName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TableID", tableID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<string> resultList = new List<string>();

                            while (reader.Read())
                            {
                                resultList.Add(reader["DatabaseName"].ToString());
                                resultList.Add( reader["TableName"].ToString());
                            }

                            return resultList;
                        }
                    }
                }
            }
            catch
            {
                throw; // Handle exceptions as needed
            }
        }

        
    }//class
   

}//namespace