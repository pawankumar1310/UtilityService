using Microsoft.Extensions.Configuration;
using Models;
using Structure;
using System.Data.SqlClient;
using System.Data;

namespace Service
{
    public class GetPhoneCodeService
    {
        private readonly IGetPhoneCodeFromID _getPhoneCodeFromID;
        public GetPhoneCodeService(IGetPhoneCodeFromID getPhoneCodeFromID) 
        {
            _getPhoneCodeFromID = getPhoneCodeFromID;
        }

        public async Task<PhoneCodeFromCountryID> PhoneCodeService(string countryID)
        {
            return await _getPhoneCodeFromID.GetPhoneCodeAsync(countryID);

        }
    }
}
