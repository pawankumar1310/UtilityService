using DBService;
using DTO.UtilityService;
using Model.UtilityService;
using Package;


namespace Service
{
    public class ZipCodeService
    {
        public StatusResponse<List<PlaceInfoByZipCodeIDResponse>> GetPlaceInfoByZipCodeID(ZipCodeIDRequest zipCodeIDRequest)
        {
            try
            {
                ZipCodeDBService zipCodeDBService = new();
                var zipCodeIDModel = new ZipCodeIDModelRequest { ZipCodeID=zipCodeIDRequest.ZipCodeID};
                var result = zipCodeDBService.GetPlaceInfoByZipCodeID(zipCodeIDModel).Result;

                if (result.Success)
                {
                    List<PlaceInfoByZipCodeIDResponse> placeInfoRequest = new();
                    foreach (var address in result.Data)
                    {
                        placeInfoRequest.Add(new PlaceInfoByZipCodeIDResponse
                        {
                            ZipCode = (long)address.ZipCode,
                            AreaName = address.AreaName,
                            CityName = address.CityName,
                            StateName = address.StateName,
                            CountryName = address.CountryName,
                            AltAreaName = address.AltAreaName,
                            AltCityName = address.AltCityName,
                            AltStateName = address.AltStateName,
                            AltCountryName = address.AltCountryName,
                        }) ;
                    }

                    return StatusResponse<List<PlaceInfoByZipCodeIDResponse>>.SuccessStatus(placeInfoRequest, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<List<PlaceInfoByZipCodeIDResponse>>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<List<PlaceInfoByZipCodeIDResponse>>.FailureStatus(StatusCode.knownException, ex);
            }
        }
        public StatusResponse<ZipCodeIDResponse> GetZipCodeIDByZipCode(ZipCodeRequest zipCodeRequest)
        {
            try
            {
                ZipCodeDBService zipCodeDBService = new();
                var zipCodeModel = new ZipCodeModelRequest { ZipCode=zipCodeRequest.ZipCode };
                var result = zipCodeDBService.GetZipCodeIDByZipCode(zipCodeModel).Result;

                if (result.Success)
                {
                    var zipcodeid = new ZipCodeIDResponse
                    {
                        ZipCodeID = result.Data?.ZipCodeID
                    };

                    return StatusResponse<ZipCodeIDResponse>.SuccessStatus(zipcodeid, StatusCode.Found);
                }
                else
                {
                    return StatusResponse<ZipCodeIDResponse>.FailureStatus(StatusCode.NotFound, new Exception());
                }
            }
            catch (Exception ex)
            {
                return StatusResponse<ZipCodeIDResponse>.FailureStatus(StatusCode.knownException, ex);
            }
        }

    }
}