using Dapper;
using DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DBService
{
    public class LocationDataAccess
    {
        private readonly string _connectionString;

        public LocationDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("UtilityDB");
        }

        public async Task<List<PlaceModel>> GetLocationInfoFromZipCodeAsync(string zipCodeID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new { zipCodeID };
                var result = await connection.QueryAsync<PlaceModel>(
                    "SP__GetLocationInfoFromZipCode",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return result.AsList();
            }
        }
    }
}
