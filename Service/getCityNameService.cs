
using Microsoft.AspNetCore.Identity;
using Structure;
using Models;

namespace Service
{
    public class GetCityNameService
    {
        private readonly ICityNames _cityNames;
        public GetCityNameService(ICityNames cityNames)
        {
            _cityNames=cityNames;
        }
        public async Task<List<CityModel>> CityNameService(string countryId )
        {
            List<CityModel> lst =await  _cityNames.GetCitiesByCountryId(countryId);
            return lst;
        }
    }

}