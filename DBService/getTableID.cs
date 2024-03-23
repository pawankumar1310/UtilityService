using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Structure;

namespace DBService
{
    public class GetTableID:ITable
    {
        private readonly IConfiguration _configuration;
        public GetTableID(IConfiguration configuration )
        {
            _configuration=configuration;
            
        }
        public async Task<string> IsValidName(string tableName)
        {            
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("GetITableIDByName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 255) { Value = tableName });

                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        return Convert.ToString(result);
                    }

                    return null; 
                }
            }
        }
        public async Task<string> IsValidNameDb(string tableName)
        {
             using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("utilityDBCS")))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetDBIDByTableName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TableName", tableName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["DatabaseID"].ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    
                }
            }
        }
    }
}