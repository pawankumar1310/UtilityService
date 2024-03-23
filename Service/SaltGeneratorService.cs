
using System.Security.Cryptography;
using Structure;
namespace Service
{
    public class SaltGeneratorService:ISalt
    {
        public byte[] GenerateRandomSaltService()
        {
            const int saltLength = 32;

            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[saltLength];
                rngCsp.GetBytes(salt);
                return salt;
            }
        }
    }
}
