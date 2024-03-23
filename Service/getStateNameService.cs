
using Microsoft.AspNetCore.Identity;
using Structure;
using Models;

namespace Service
{
    public class getStateNameService
    {
        private readonly IStateNames _stateNames;
        public getStateNameService(IStateNames stateNames)
        {
            _stateNames=stateNames;
        }
        public async Task<List<StateModel>> StateNameService(string countryId )
        {
            List<StateModel> lst =await  _stateNames.GetStatesByCountryId(countryId);
            return lst;


        }
    }

}