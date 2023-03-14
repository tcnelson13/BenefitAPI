using System;
using BenefitsCalculation.API.Entities;

namespace BenefitsCalculation.API.Repository;

public interface IPayrollAdminRepository
{
    Task<List<BenefitRate>> GetBenefitRatesAsync();
    Task<int> UpdateBenefitRateAsync(BenefitRate benefitRate);
}

