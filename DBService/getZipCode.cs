using Structure;
using System.Data.SqlClient;
using System.Data;
using Models;
namespace DBService
{
    public class GetZipCode : IZipCodeNames
    {
        private readonly IConfiguration _configuration;
        public GetZipCode(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<ZipCodeModel>> GetZipCodesByCountryId(string countryId)
        {
            List<ZipCodeModel> zipCodes = new List<ZipCodeModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetZipCodesByCountryId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.VarChar) { Value = countryId });

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

        public async Task<string> GetZipCodeIdByCode(long zipCode)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                using (var command = new SqlCommand("spGetZipCodeIdByCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@zipCode", zipCode);

                    connection.Open();

                    var result = command.ExecuteScalar();

                    return result != null ? result.ToString() : null;
                }
            }
        }

        public async Task<string> GetZipCodeByZipCodeID(string zipCodeId)
        {
             using (var connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                using (var command = new SqlCommand("SP__GetZipCodeByZipCodeId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ZipCodeId", zipCodeId);

                    connection.Open();

                    var result = command.ExecuteScalar();

                    return result != null ? result.ToString() : null;
                }
            }
        }
    }
}