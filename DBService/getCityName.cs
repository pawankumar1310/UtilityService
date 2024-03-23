

using Structure;
using System.Data.SqlClient;
using System.Data;
using Models;
namespace DBService
{
    public class GetCityName : ICityNames
    {
        private readonly IConfiguration _configuration;
        public GetCityName(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        public async Task<List<CityModel>> GetCitiesByCountryId(string countryId)
        {
            List<CityModel> cities = new List<CityModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("utilityDBCS")))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetCitiesByCountryId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.VarChar) { Value = countryId });

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CityModel city = new CityModel
                            {
                                CityID = reader["cityID"].ToString(),
                                Name = reader["name"].ToString(),
                            };

                            cities.Add(city);
                        }
                    }
                }
            }

            return cities;
        }
    }
}