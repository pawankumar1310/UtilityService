using Models;
using Structure;
using System.Data.SqlClient;
using System.Data;

namespace DBService
{
    public class GetPhoneCode:IPhoneCode
    {
        private readonly IConfiguration _configuration;
        public GetPhoneCode(IConfiguration configuration )
        {
            _configuration=configuration;
            
        }
        public async Task<List<PhoneCodeModel>> GetPhoneNumberCode()
        {
            List<PhoneCodeModel> phoneCode = new List<PhoneCodeModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetCountryData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            phoneCode.Add(new PhoneCodeModel
                            {
                                CountryID = reader["countryID"].ToString(),

                                Name = reader["name"].ToString(),
                                
                                PhoneCode = reader["phoneCode"].ToString(),
                            });
                        }
                    }
                }
            }

            return phoneCode;
        }
    }

}