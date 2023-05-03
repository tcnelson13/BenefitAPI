using BenefitsCalculation.API.Entities;

namespace BenefitsCalculation.API.Logic
{
    public interface IBenefitCalc
    {
        double CalculateDiscount(string name, double annualBenefitCost);
        double CalculateTotalBenefitCost(Employee employee, List<BenefitRate> benefitRates);
        Task<int?> UpdateBenefitCostAsync(int employeeId);
    }
}