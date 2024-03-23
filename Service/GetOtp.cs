using Structure;

namespace UtilityService.Service
{
    public class GetOtp:IGenerateOtp
    {
        public async Task<int> OtpService()
        {
            Random random = new Random();
            return random.Next(100000, 999999);

        }

    }
}
