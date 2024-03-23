using System.Reflection.Emit;
using Models;
namespace Structure
{
    public interface IZipOfState
    {
        public Task<List<ZipCodeModel>> GetZipCodesByStateId(string stateId);
    }
}
