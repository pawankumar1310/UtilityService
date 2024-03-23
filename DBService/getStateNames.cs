
using Models;
using Structure;
using System.Data.SqlClient;
using System.Data;

namespace DBService
{
    public class GetStateNames : IStateNames
    {
        private readonly IConfiguration _configuration;
        public GetStateNames(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        public async Task<List<StateModel>> GetStatesByCountryId(string countryId)
        {
              
            List<StateModel> states = new List<StateModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("GetStatesByCountryId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CountryID", SqlDbType.VarChar) { Value = countryId });

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            StateModel state = new StateModel
                            {
                                StateID = reader["stateID"].ToString(),
                                Name = reader["name"].ToString(),
                            };

                            states.Add(state);
                        }
                    }
                }
            }

            return states;
            }
    }
}