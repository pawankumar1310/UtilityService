using Middleware;
using Constants.StoredProcedure;
using Model.UtilityService;
using Package;
using System.Data.SqlClient;


namespace DBService
{
    public class ZipCodeDBService
    {
        string connectionString = Utility.ConfigurationUtility.GetConnectionString();
        public async Task<StatusResponse<List<PlaceInfoByZipCodeIDModelResponse>>> GetPlaceInfoByZipCodeID(ZipCodeIDModelRequest zipCodeIDModelRequest)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UtilityDB.GetPlaceInfoByZipCode;
                var parameter = new SqlParameter[] {
                    new SqlParameter("@zipCodeID",zipCodeIDModelRequest.ZipCodeID)
                };

                var result = await curdMiddleware.ExecuteDataReaderList<PlaceInfoByZipCodeIDModelResponse>(connectionString, storedProcedure, (reader) => new PlaceInfoByZipCodeIDModelResponse
                {
                    ZipCode = (long)reader["zipCode"],
                    AreaName = reader["areaName"] == DBNull.Value ? null : reader["areaName"].ToString(),
                    AltAreaName = reader["altAreaName"] == DBNull.Value ? null : reader["altAreaName"].ToString(),
                    CityName = reader["cityName"] == DBNull.Value ? null : reader["cityName"].ToString(),
                    AltCityName = reader["altCityName"] == DBNull.Value ? null : reader["altCityName"].ToString(),
                    StateName = reader["stateName"] == DBNull.Value ? null : reader["stateName"].ToString(),
                    AltStateName = reader["altStateName"] == DBNull.Value ? null : reader["altStateName"].ToString(),
                    CountryName = reader["countryName"] == DBNull.Value ? null : reader["countryName"].ToString(),
                    AltCountryName = reader["altCountryName"] == DBNull.Value ? null : reader["altCountryName"].ToString()
                },parameter);
                if (result != null)
                {
                    return StatusResponse<List<PlaceInfoByZipCodeIDModelResponse>>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<List<PlaceInfoByZipCodeIDModelResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<PlaceInfoByZipCodeIDModelResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public async Task<StatusResponse<ZipCodeIDModelResponse>> GetZipCodeIDByZipCode(ZipCodeModelRequest zipCodeModelRequest)
        {
            try
            {
                CurdMiddleware curdMiddleware = new();
                var storedProcedure = UtilityDB.GetZipCodeIDByZipCode;
                var parameter = new SqlParameter[] {
                    new SqlParameter("@zipCode",zipCodeModelRequest.ZipCode)
                };
                var result=await curdMiddleware.ExecuteDataReaderSingle<ZipCodeIDModelResponse>(connectionString,storedProcedure,(reader)=>new ZipCodeIDModelResponse
                {
                    ZipCodeID = reader["zipCodeID"] == DBNull.Value ? null : reader["zipCodeID"].ToString(),
                },parameter);
                if (result!= null)
                {
                    return StatusResponse<ZipCodeIDModelResponse>.SuccessStatus(result, StatusCode.Success);
                }
                else
                {
                    return StatusResponse<ZipCodeIDModelResponse>.FailureStatus(StatusCode.NotFound, new Exception());
                }

            }
            catch (Exception ex)
            {
                return StatusResponse<ZipCodeIDModelResponse>.FailureStatus(StatusCode.knownException, ex);
            }
        }
    }
}