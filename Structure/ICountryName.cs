using Models;
namespace Structure
{
    public interface ICountryName
    {
        public Task<List<CountryAndIDModel>> GetAllCountryName();
    } 

}