using BenefitsCalculation.API.Repository;
using BenefitsCalculation.API.Entities;
using BenefitsCalculation.API.Models;

namespace BenefitsCalculation.API.Logic;

public class PayrollAdminLogic : IPayrollAdminLogic
{
    private readonly IPayrollAdminRepository _payrollAdminRepository;
    private readonly IEmployeeLogic _employeeLogic;
    private readonly IBenefitCalc _benefitCalc;

    public PayrollAdminLogic(
        IPayrollAdminRepository payrollRepository,
        IEmployeeLogic employeeLogic,
        IBenefitCalc benefitCalc)
    {
        _payrollAdminRepository = payrollRepository;
        _employeeLogic = employeeLogic;
        _benefitCalc = benefitCalc;
    }

    public async Task<int> UpdateBenefitRateAsync(BenefitRateModel benefitRateModel)
    {
        BenefitRate benefitRate = BenefitRateModel.ToBenefitRate(benefitRateModel);
        int rowsAffected = await _payrollAdminRepository.UpdateBenefitRateAsync(benefitRate);
        return rowsAffected;
    }

    public async Task<List<BenefitRateModel>> GetBenefitRatesAsync()
    {
        var benefitRates = await _payrollAdminRepository.GetBenefitRatesAsync();
        List<BenefitRateModel> benefitRateModels = new List<BenefitRateModel>();

        foreach (var benefitRate in benefitRates)
        {
            benefitRateModels.Add(BenefitRateModel.FromBenefitRate(benefitRate));
        }

        return benefitRateModels;
    }

    public async Task<List<EmployeeModel>> PreviewPayrollAsync()
    {
        List<EmployeeModel> employeeModels = await _employeeLogic.GetEmployeesAsync();
        List<BenefitRate> benefitRates = await _payrollAdminRepository.GetBenefitRatesAsync();

        foreach (var employeeModel in employeeModels)
        {
            var annualBenefitCost = CalculateTotalBenefitCost(employeeModel, benefitRates);
            employeeModel.PayPeriodBenefitCost = (annualBenefitCost / employeeModel.AnnualPaycheckCount);
        }        

        return employeeModels;
    }

    public async Task<List<EmployeeModel>> ProcessPayrollAsync()
    {
        List<EmployeeModel> employeeModels = await _employeeLogic.GetEmployeesAsync();
        
        foreach (var employeeModel in employeeModels)
        {
            int? rowsAffected = await _benefitCalc.UpdateBenefitCostAsync((int)employeeModel.EmployeeId);
        }

        List<EmployeeModel> employeeModelsProcessed = await _employeeLogic.GetEmployeesAsync();

        return employeeModelsProcessed;
    }

    public double CalculateTotalBenefitCost(EmployeeModel employeeModel, List<BenefitRate> benefitRates)
    {
        double totalAnnualdBenefitCost = 0;
        double annualEmployeeBenefitCost = benefitRates.Find(er => er.BenefitRateId == 1).AnnualBenefitCost;
        double annualDependentBenefitCost = benefitRates.Find(dr => dr.BenefitRateId == 2).AnnualBenefitCost;

        totalAnnualdBenefitCost += _benefitCalc.CalculateDiscount(employeeModel.Name, annualEmployeeBenefitCost);

        foreach (var dependentModel in employeeModel.Dependents)
        {
            totalAnnualdBenefitCost += _benefitCalc.CalculateDiscount(dependentModel.Name, annualDependentBenefitCost);
        }   

        return totalAnnualdBenefitCost;
    }
}

