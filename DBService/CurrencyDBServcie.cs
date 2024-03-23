using System.Data.SqlClient;
using System.Data;
using DTO;

namespace DBService
{
    public class CurrencyDBServcie
    {
        public readonly IConfiguration _configuration;

        public CurrencyDBServcie(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<CurrencyModel>> GetConversionRates()
        {
            List<CurrencyModel> countries = new List<CurrencyModel>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("UtilityDB")))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SP__GetConversionRateWithSymbolAndCurrencyCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (await reader.ReadAsync())
                        {
                            CurrencyModel country = new CurrencyModel
                            {
                                CurrencySymbol = reader["currencySymbol"] is DBNull ? null : reader["currencySymbol"].ToString(),
                                ISOCurrencyCode = reader["ISOCurrencyCode"] is DBNull ? null : reader["ISOCurrencyCode"].ToString(),
                                ConversionRate = reader["ConversionRate"] is DBNull ? (decimal?)null : Convert.ToDecimal(reader["ConversionRate"])
                            };

                            countries.Add(country);
                        }
                    }
                }
            }

            return countries;
        }
    }
}
