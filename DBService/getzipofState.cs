using System.Data.SqlClient;
using System.Data;
using System.Reflection.Emit;
using Models;
using Structure;

namespace DBService
{
    public class GetZipOfState:IZipOfState
    {
        private readonly IConfiguration _configuration;
        public GetZipOfState(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<ZipCodeModel>> GetZipCodesByStateId(string stateId)
        {
            List<ZipCodeModel> zipCodes = new List<ZipCodeModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetZipCodesByStateId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@StateID", SqlDbType.VarChar) { Value = stateId });

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ZipCodeModel zipCode = new ZipCodeModel
                            {
                                ZipCodeID = reader["zipCodeID"].ToString(),
                                ZipCodeValue = Convert.ToInt64(reader["zipCode"]),
                                AreaName = reader["areaName"].ToString(),
                            };

                            zipCodes.Add(zipCode);
                        }
                    }
                }
            }

            return zipCodes;
        }
    }
}
