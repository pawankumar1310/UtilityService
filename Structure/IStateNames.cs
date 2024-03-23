using Models;
namespace Structure
{
    public interface IStateNames
    {
        public  Task<List<StateModel>> GetStatesByCountryId(string countryId);
    }

}