using Structure;
using Models;

namespace Service
{
  
        public class getZipofStateService
        {
            private readonly IZipOfState _zipstate;
            public getZipofStateService(IZipOfState zipstate)
            {
            _zipstate = zipstate;
            }
            public async Task<List<ZipCodeModel>> ZipOfState(string stateID)
            {
                List<ZipCodeModel> lst = await _zipstate.GetZipCodesByStateId(stateID);
                return lst;


            }
        }
    
}
