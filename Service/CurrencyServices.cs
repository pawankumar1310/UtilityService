using DBService;
using DTO;

namespace Service
{
    public class CurrencyServices
    {
        public readonly CurrencyDBServcie _currencyDBServcie;

        public CurrencyServices(CurrencyDBServcie currencyDBServcie)
        {
            _currencyDBServcie = currencyDBServcie;  
        }
        
        public async Task<List<CurrencyModel>> GetListOfCurrencyList()
        {
            List<CurrencyModel> lst = await _currencyDBServcie.GetConversionRates();
            return lst; 
        }
    }
}
