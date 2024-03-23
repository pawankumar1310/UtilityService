using Models;
namespace Structure
{
    public interface IZipCodeNames
    {
        public  Task<List<ZipCodeModel>> GetZipCodesByCountryId(string countryId);
        public  Task<string> GetZipCodeIdByCode(long zipCode);

        public Task<string> GetZipCodeByZipCodeID(string zipCodeId);
    }
}