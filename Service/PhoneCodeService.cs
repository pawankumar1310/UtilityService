using Models;
using Structure;

namespace Service
{
    public class PhoneCodeService
    {
        private readonly IPhoneCode iPhoneCode;

        public PhoneCodeService(IPhoneCode iPhoneCode)
        {
            this.iPhoneCode=iPhoneCode;
           
        }
        public async Task<List<PhoneCodeModel>> getPhoneCodeService()
        {
            List<PhoneCodeModel> phoneCodeModels=await iPhoneCode.GetPhoneNumberCode();
            return phoneCodeModels;

        }

    }
}