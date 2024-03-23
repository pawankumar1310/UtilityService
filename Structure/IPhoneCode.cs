using Models;
namespace Structure
{
    public interface IPhoneCode
    {
        public  Task<List<PhoneCodeModel>> GetPhoneNumberCode();
    }

}