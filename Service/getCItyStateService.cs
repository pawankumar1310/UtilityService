
using Microsoft.AspNetCore.Identity;
using Structure;
using Models;

namespace Service
{
    public class GetCityStateService
    {
        private readonly ICityState _cityNames;
        public GetCityStateService(ICityState cityNames)
        {
            _cityNames = cityNames;
        }
        public async Task<List<CityModel>> CityofStateService(string stateID)
        {
            List<CityModel> lst = await _cityNames.GetCitiesByStateId(stateID);
            return lst;


        }
    }

}
