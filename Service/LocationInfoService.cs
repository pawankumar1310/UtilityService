// CityService.cs
using DBService;
using DTO;


namespace Service{

    public class LocationInfoService
    {
        private readonly LocationInfoDBService _locationInfoDBService;

        public LocationInfoService(LocationInfoDBService LocationInfoDBService)
        {
            _locationInfoDBService = LocationInfoDBService;
        }


    // ---------------------------------------- GET ALL CITIES -----------------------------------------------


        public async Task<List<City>> GetAllCitiesAsync()
        {
            return await _locationInfoDBService.GetAllCities();
        }


    // ------------------------------------- GET COUNTRY, STATE, CITY, AREA BY ZIPCODE --------------------------------------

        public async Task<LocationInfo> GetLocationInfoByZipCodeAsync(string zipCode)
        {
            return await _locationInfoDBService.GetLocationInfoByZipCode(zipCode);
        }


    // ---------------------------------------- GET STATE, COUNTRY BY CITY -------------------------------------


        public async Task<StateCountry> GetStateAndCountryByCityIdAsync(Guid cityId)
        {
            return await _locationInfoDBService.GetStateAndCountryByCityId(cityId);
        }


    // ------------------------------------------- GET COUNTRY BY STATE --------------------------------------------


        public async Task<StateCountry> GetCountryByStateNameAsync(string stateName)
        {
            return await _locationInfoDBService.GetCountryByStateName(stateName);
        }


    // ------------------------------------------- GET ZIP CODES BY CITY NAME ------------------------------------------------


        public async Task<List<ZipCodes>> GetZipCodesByCityNamesAsync(List<string> cityNames)
        {
            return await _locationInfoDBService.GetZipCodesByCityNames(cityNames);
        }


    //------------------------------------------------ GET AREA BY ZIPCODE --------------------------------------------------

        public async Task<LocationInfo> GetAreaByZipCodeAsync(string zipcode)
        {
            return await _locationInfoDBService.GetAreaByZipCode(zipcode);
        }

    }
}
