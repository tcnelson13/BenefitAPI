using System;
using BenefitsCalculation.API.Entities;
using Microsoft.Data.Sqlite;

namespace BenefitsCalculation.API.Repository
{
	public class PayrollAdminRepository  : IPayrollAdminRepository
	{
        private string GetConnectionString()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dbPath = Path.Join(path, "payroll.db");
            string connectionString = string.Format("Data Source={0};", dbPath);
            return connectionString;
        }

        public PayrollAdminRepository()
		{
		}

        public Task<BenefitRate> GetBenefitRateAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<BenefitRate>> GetBenefitRatesAsync()
        {
            List<BenefitRate> benefitRates = new List<BenefitRate>();
            try
            {
                using var connection = new SqliteConnection(GetConnectionString());
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                SELECT BenefitRateId, Name, AnnualBenefitCost
                FROM BenefitRate;
                ";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var benefitRate = new BenefitRate();
                        benefitRate.BenefitRateId = (long)reader["BenefitRateId"];
                        benefitRate.Name = reader["Name"].ToString();
                        benefitRate.AnnualBenefitCost = (long)reader["AnnualBenefitCost"];
                        benefitRates.Add(benefitRate);
                    }
                }
            }
            catch(Exception ex)
            {
                // Log exception
            }
            return benefitRates;
        }

        public async Task<int> UpdateBenefitRateAsync(BenefitRate benefitRate)
        {
            int rowsAffected = 0;
            try
            {
                using var connection = new SqliteConnection(GetConnectionString());
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                UPDATE BenefitRate SET
                    AnnualBenefitCost = $AnnualBenefitCost
                WHERE BenefitRateId = $BenefitRateId;
                ";
                command.Parameters.AddWithValue("$BenefitRateId", benefitRate.BenefitRateId);
                command.Parameters.AddWithValue("$AnnualBenefitCost", benefitRate.AnnualBenefitCost);
                rowsAffected = await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                // Log exception
            }
            return rowsAffected;
        }
    }
}

