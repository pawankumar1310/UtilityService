using System.Data.SqlClient;
using System.Data;

namespace DBService
{
    public class UpdateCurrencyDbService
    {
     
        public async Task UpdateDatabaseAsync(dynamic exchangeRateInfo)
        {
            string conn = "Server=192.168.0.200\\MSSQLSERVERENT;Database=UtilityDB;User Id=sa;Password=tagme!23;";
            using (SqlConnection connection = new SqlConnection(conn))
            {
                await connection.OpenAsync();

                using (SqlCommand selectCommand = new SqlCommand("SP__GetCountryISOCurrencyCodes", connection))
                {
                    selectCommand.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string ISOCurrencyCode = reader["ISOCurrencyCode"].ToString();

                            if (exchangeRateInfo.conversion_rates.ContainsKey(ISOCurrencyCode))
                            {
                                decimal conversionRate = exchangeRateInfo.conversion_rates[ISOCurrencyCode];

                                await UpdateConversionRateAsync(ISOCurrencyCode, conversionRate);
                            }
                        }
                    }
                }
            }
        }

        public async Task UpdateConversionRateAsync(string ISOCurrencyCode, decimal conversionRate)
        {
            string conn1 = "Server=192.168.0.200\\MSSQLSERVERENT;Database=UtilityDB;User Id=sa;Password=tagme!23;";
            using (SqlConnection connection1 = new SqlConnection(conn1))
            {
                await connection1.OpenAsync();

                using (SqlCommand updateCommand = new SqlCommand("SP__UpdateCountryConversionRate", connection1))
                {
                    updateCommand.CommandType = CommandType.StoredProcedure;
                    updateCommand.Parameters.Add("@ISOCurrencyCode", SqlDbType.NVarChar, 10).Value = ISOCurrencyCode;
                    updateCommand.Parameters.Add("@conversionRate", SqlDbType.Money).Value = conversionRate;

                    await updateCommand.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

