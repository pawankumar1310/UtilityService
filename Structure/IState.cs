

using Models;

namespace Structure
{
    public interface IState
    {
        public Task<List<StateModel>> GetStateIdAndName();
    }
}