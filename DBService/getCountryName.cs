using Structure;

using System.Data.SqlClient;
using System.Data;
using Models;
namespace DBService
{
    public class GetCountryName : ICountryName
    {
        private readonly IConfiguration _configuration;
        public GetCountryName(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        public async  Task<List<CountryAndIDModel>> GetAllCountryName()
        {
            
            List<CountryAndIDModel> countryNames = new List<CountryAndIDModel>();
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                await connection.OpenAsync();

                using (SqlCommand command =new SqlCommand("GetAllCountries",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CountryAndIDModel country = new CountryAndIDModel
                            {
                                CountryID = reader["countryID"].ToString(),
                                Name      = reader["name"].ToString(),
                                phoneCode = reader["phoneCode"].ToString()  
                            };

                            countryNames.Add(country);
                        }
                    }
                }
        

                return countryNames;
            }
        }
    }
}