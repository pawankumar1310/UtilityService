
using Microsoft.AspNetCore.Identity;
using Structure;
using Models;

namespace Service
{
    public class getZipCodeService
    {
        private readonly IZipCodeNames _zipCodeNames;
        public getZipCodeService(IZipCodeNames zipCodeNames)
        {
            _zipCodeNames=zipCodeNames;
        }
        public async Task<List<ZipCodeModel>> ZipService(string countryId )
        {
            List<ZipCodeModel> lst =await  _zipCodeNames.GetZipCodesByCountryId(countryId);
            return lst;


        }
        public async Task<String> GetZipCodeID(long zipCode)
        {
            string result=await _zipCodeNames.GetZipCodeIdByCode(zipCode);
            return result;
        }

        public async Task<string> GetZipCodeByZipCodeID(string zipCodeId)
        {
            return await _zipCodeNames.GetZipCodeByZipCodeID(zipCodeId);
        }
           
    }

}