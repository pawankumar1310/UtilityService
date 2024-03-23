
using System.Data.SqlClient;
using System.Data;
using Models;
using Structure;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace DBService
{
    public class GetStateId : IState
    {
        private readonly IConfiguration _configuration;
        public GetStateId(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public async Task<List<StateModel>> GetStateIdAndName()
        {
            List<StateModel> stateName=new List<StateModel>();
            using(SqlConnection connection=new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                await connection.OpenAsync();
                using(SqlCommand command=new SqlCommand("GetAllState",connection))
                {
                    command.CommandType=CommandType.StoredProcedure;
                    using(SqlDataReader reader=await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            StateModel state=new StateModel
                            {
                                StateID=reader["stateID"].ToString(),
                                Name =reader["name"].ToString(),

                            };
                            stateName.Add(state);
                        }
                    }

                }
                return stateName;
            }
            
        }
    }
}