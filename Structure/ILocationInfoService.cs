using DTO;

namespace Structure {
    interface ILocationInfoService{
    public  Task<List<City>> GetAllCities();
    public  Task<LocationInfo> GetLocationInfoByZipCode(string zipCode);
    public  Task<StateCountry> GetStateAndCountryByCityId(Guid cityId);
    public  Task<StateCountry> GetCountryByStateName(string stateName);
    public  Task<List<ZipCodes>> GetZipCodesByCityNames(List<string> cityNames);
    public Task<LocationInfo> GetAreaByZipCode(string zipcode);
    }
}