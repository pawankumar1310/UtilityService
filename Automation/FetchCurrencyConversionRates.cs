using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using DBService;

namespace Automation
{
    public class FetchCurrencyConversionRates
    {
       
        public async Task UpdateExchangeRatesAsync()
        {
            try
            {
                HttpResponseMessage response = await GetExchangeRateInfoAsync();

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var exchangeRateInfo = JsonConvert.DeserializeObject<dynamic>(content);
                    UpdateCurrencyDbService udb=new UpdateCurrencyDbService();
                    await udb.UpdateDatabaseAsync(exchangeRateInfo);
                    Console.WriteLine("Conversion rates updated in the database successfully.");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private async Task<HttpResponseMessage> GetExchangeRateInfoAsync()
        {
            string apiUrl = "https://v6.exchangerate-api.com/v6/aa5526e1369018a1c9d4b528/latest/USD";
            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync(apiUrl);
            }
        }

    }
}
