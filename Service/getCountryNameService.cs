using Structure;
using Models;

namespace Service
{
    public class GetCountryNameService
    {
        private readonly ICountryName _countryName;
        public GetCountryNameService(ICountryName countryName)
        {
            _countryName=countryName;
        }

        public async Task<List<CountryAndIDModel>> GetCountryService()
        {
            List<CountryAndIDModel> lst =await _countryName.GetAllCountryName();
            return lst;
        } 

    }

}