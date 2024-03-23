
using Models;
namespace Structure
{
    public interface ICityState
    {
        public Task<List<CityModel>> GetCitiesByStateId(string stateID);
    }
}