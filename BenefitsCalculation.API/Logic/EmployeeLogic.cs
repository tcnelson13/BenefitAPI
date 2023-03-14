using BenefitsCalculation.API.Repository;
using BenefitsCalculation.API.Entities;
using BenefitsCalculation.API.Models;

namespace BenefitsCalculation.API.Logic;

public class EmployeeLogic : IEmployeeLogic
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPayrollAdminRepository _payrollAdminRepository;
    private readonly double discount = .10;
    private readonly char discountChar = 'A';


    public EmployeeLogic(IEmployeeRepository employeeRepository, IPayrollAdminRepository payrollAdminRepository)
	{
        _employeeRepository = employeeRepository;
        _payrollAdminRepository = payrollAdminRepository;
	}

    public async Task<int> AddDependentAsync(int employeeId, DependentModel dependentModel)
    {
        var dependent = DependentModel.ToDependent(dependentModel);
        int dependentId = await _employeeRepository.AddDependentAsync(employeeId, dependent);

        return dependentId;
    }

    public async Task<EmployeeModel?> GetEmployeeAsync(int employeeId)
	{
		var employee = await _employeeRepository.GetEmployeeAsync(employeeId);
        return employee == null ? null : EmployeeModel.FromEmployee(employee);
	}

    public async Task<List<EmployeeModel>> GetEmployeesAsync()
    {
        var employees = await _employeeRepository.GetEmployeesAsync();
        List<EmployeeModel> employeeModels = new List<EmployeeModel>();

        foreach (var employee in employees)
        {
            employeeModels.Add(EmployeeModel.FromEmployee(employee));
        }

        return employeeModels;
    }

    public async Task<int?> UpdateBenefitCostAsync(int employeeId)
    {
        var employee = await _employeeRepository.GetEmployeeAsync(employeeId);

        if (employee == null)
        {
            return null;
        }
        List<BenefitRate> benefitRates = await _payrollAdminRepository.GetBenefitRatesAsync();
        double totalPayPeriodBenefitCost = CalculateTotalBenefitCost(employee, benefitRates);    

        totalPayPeriodBenefitCost = (totalPayPeriodBenefitCost / employee.AnnualPaycheckCount);

        var updatedEmployee = await _employeeRepository.UpdateBenefitCost(employeeId, totalPayPeriodBenefitCost);
        
        return updatedEmployee;
    }

    public double CalculateTotalBenefitCost(Employee employee, List<BenefitRate> benefitRates)
    {
        double totalAnnualdBenefitCost = 0;
        double annualEmployeeBenefitCost = benefitRates.Find(er => er.BenefitRateId == 1).AnnualBenefitCost;
        double annualDependentBenefitCost = benefitRates.Find(dr => dr.BenefitRateId == 2).AnnualBenefitCost;
        totalAnnualdBenefitCost += CalculateDiscount(employee.Name, annualEmployeeBenefitCost);

        foreach (var dependent in employee.Dependents)
        {
            totalAnnualdBenefitCost += CalculateDiscount(dependent.Name, annualDependentBenefitCost);
        }

        return totalAnnualdBenefitCost;
    }

    public double CalculateDiscount(string name, double annualBenefitCost)
    {
        return name != null && name.StartsWith(discountChar) ?
            (annualBenefitCost -  (annualBenefitCost * discount)) :
            annualBenefitCost;
    }
}
