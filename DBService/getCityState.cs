

using Structure;
using System.Data.SqlClient;
using System.Data;
using Models;
namespace DBService
{
    public class GetCityState : ICityState
    {
        private readonly IConfiguration _configuration;
        public GetCityState(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        public async Task<List<CityModel>> GetCitiesByStateId(string stateId)
    {
        List<CityModel> cities = new List<CityModel>();

        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
        {
            await connection.OpenAsync();

            using (SqlCommand command = new SqlCommand("GetCitiesByStateId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@StateID", SqlDbType.VarChar) { Value = stateId });

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