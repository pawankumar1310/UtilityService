using Models;
using Structure;
using System.Data.SqlClient;
using System.Data;
using DTO;


namespace DBService
{
    public class GetPlaceInformation : IPlaceInformation
    {
        private readonly IConfiguration _configuration;
        public GetPlaceInformation(IConfiguration configuration )
        {
            _configuration=configuration;
            
        }

        public async Task<List<PlaceInformationModel>> GetAllPlaceInformation(string zipCodeId)
        {
            List<PlaceInformationModel> placeInformationList = new List<PlaceInformationModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetPlaceInformationByZipCodeID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@zipCodeID", zipCodeId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync())
                        {
                            placeInformationList.Add(MapToPlaceInformation(reader));
                        }
                    }
                }
            }

            return placeInformationList;

        }
        private PlaceInformationModel MapToPlaceInformation(SqlDataReader reader)
        {
            return new PlaceInformationModel
            {
                ZipCode = (long)reader["zipCode"],
                AreaName = reader["AreaName"] == DBNull.Value ? null : reader["AreaName"].ToString(),
                CityName = reader["CityName"] == DBNull.Value ? null : reader["CityName"].ToString(),
                StateName = reader["StateName"] == DBNull.Value ? null : reader["StateName"].ToString(),
                CountryName = reader["CountryName"] == DBNull.Value ? null : reader["CountryName"].ToString(),
                AltAreaName = reader["AltAreaName"] == DBNull.Value ? null : reader["AltAreaName"].ToString(),
                AltCityName = reader["AltCityName"] == DBNull.Value ? null : reader["AltCityName"].ToString(),
                AltStateName = reader["AltStateName"] == DBNull.Value ? null : reader["AltStateName"].ToString(),
                AltCountryName = reader["AltCountryName"] == DBNull.Value ? null : reader["AltCountryName"].ToString()
            };
        }

        public async Task<List<PlaceModel>> GetLocationInfoFromZipCodeAsync(string zipCodeID)
        {
            List<PlaceModel> result = new List<PlaceModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SP__GetLocationInfoFromZipCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@zipCodeID", zipCodeID);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new PlaceModel
                            {
                                AreaName = reader["areaName"].ToString(),
                                CityName = reader["cityName"].ToString(),
                                StateName = reader["stateName"].ToString(),
                                CountryName = reader["countryName"].ToString(),
                            });
                        }
                    }
                }
            }

            return result;
        }






    }

}