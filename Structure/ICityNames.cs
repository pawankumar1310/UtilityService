using Models;
namespace Structure
{
    public interface ICityNames
    {
        public Task<List<CityModel>> GetCitiesByCountryId(string countryId);
    }
}