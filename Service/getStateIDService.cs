

using Models;
using Structure;

namespace Service
{
    public class getStateIdService
    {
        private readonly IState _state;

        public getStateIdService(IState state)
        {
            _state=state;
        }

        public async Task<List<StateModel>> GetStateNameAndIdService()
        {
            List<StateModel> lst=await _state.GetStateIdAndName();
            return lst;

        }
    }

}