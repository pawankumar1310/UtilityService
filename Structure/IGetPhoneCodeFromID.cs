using Models;

namespace Structure
{
    public interface IGetPhoneCodeFromID
    {
        public  Task<PhoneCodeFromCountryID> GetPhoneCodeAsync(string countryID);
    }
}
