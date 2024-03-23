// ZipCodeRepository.cs

using System.Data;
using System.Data.SqlClient;
using DTO;
using Structure;

namespace DBService{
    public class LocationInfoDBService : ILocationInfoService
    {
        private readonly IConfiguration _configuration;

        public LocationInfoDBService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    // ------------------------------------- GET COUNTRY, STATE, CITY, AREA BY ZIPCODE ------------------------------------------------

        public async Task<LocationInfo> GetLocationInfoByZipCode(string zipCode)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                using (SqlCommand command = new SqlCommand("SP_GetLocationInfoByZipCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ZipCode", zipCode);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new LocationInfo
                            {
                                AreaName = reader.GetString(reader.GetOrdinal("areaName")),
                                //CityID = reader.GetGuid(reader.GetOrdinal("cityID")),
                                CityName = reader.GetString(reader.GetOrdinal("cityName")),
                                //StateID = reader.GetGuid(reader.GetOrdinal("stateID")),
                                StateName = reader.GetString(reader.GetOrdinal("stateName")),
                                //CountryID = reader.GetGuid(reader.GetOrdinal("countryID")),
                                CountryName = reader.GetString(reader.GetOrdinal("countryName")),
                            };
                        }
                    }
                }
            }

            return null;
        }


    // ---------------------------------------- GET STATE, COUNTRY BY CITY -------------------------------------


    public async Task<StateCountry> GetStateAndCountryByCityId(Guid cityId)
    {
        using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
        {
            using (SqlCommand command = new SqlCommand("SP_GetStateAndCountryByCityId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CityId", cityId);

                await connection.OpenAsync();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new StateCountry
                        {
                            State = reader["stateName"].ToString(),
                            Country = reader["countryName"].ToString()
                        };
                    }
                }
            }
        }

        return null;
    }



    // ---------------------------------------- GET ALL CITIES -----------------------------------------------


        public async Task<List<City>> GetAllCities()
        {
            List<City> cities = new List<City>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllCities", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (reader.Read())
                        {
                            City city = new City
                            {
                                CityID = Guid.Parse(reader["cityID"].ToString()),
                                Name = reader["name"].ToString(),
                            
                                // Add other properties as needed
                            };

                            cities.Add(city);
                        }
                    }
                }
            }

            return cities;
        }


    // ------------------------------------------- GET COUNTRY BY STATE --------------------------------------------

        public async Task<StateCountry> GetCountryByStateName(string stateName)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                using (SqlCommand command = new SqlCommand("SP_GetCountryByStateName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StateName", stateName);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new StateCountry
                            {
                                Country = reader["countryName"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }


    // ------------------------------------------- GET ZIP CODES BY CITY NAME ------------------------------------------------



        public async Task<List<ZipCodes>> GetZipCodesByCityNames(List<string> cityNames)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                using (SqlCommand command = new SqlCommand("SP_GetZipCodesByCityNames", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    var cityNamesTable = new DataTable();
                    cityNamesTable.Columns.Add("CityName", typeof(string));

                    foreach (var cityName in cityNames)
                    {
                        cityNamesTable.Rows.Add(cityName);
                    }

                    SqlParameter parameter = command.Parameters.AddWithValue("@CityNames", cityNamesTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "CityNameList";

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<ZipCodes> zipCodes = new List<ZipCodes>();

                        while (await reader.ReadAsync())
                        {
                            zipCodes.Add(new ZipCodes
                            {
                                ZipCode = reader["zipCode"].ToString(),
                            });
                        }

                        return zipCodes;
                    }
                }
            }
        }


    //------------------------------------------------ GET AREA BY ZIPCODE --------------------------------------------------

        public async Task<LocationInfo> GetAreaByZipCode(string zipcode)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAreaByZipCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ZipCode", zipcode);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new LocationInfo
                            {
                                AreaName = reader["areaName"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }
    }
}
