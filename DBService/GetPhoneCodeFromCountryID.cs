using Models;
using System.Data.SqlClient;
using System.Data;
using Structure;

namespace DBservice
{
    public class GetPhoneCodeFromCountryID : IGetPhoneCodeFromID
    {
        private readonly IConfiguration _configuration;
        public GetPhoneCodeFromCountryID(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<PhoneCodeFromCountryID> GetPhoneCodeAsync(string countryID)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand("spGetPhoneCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.VarChar) { Value = countryID });

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new PhoneCodeFromCountryID
                            {
                                PhoneCode = reader["phoneCode"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
