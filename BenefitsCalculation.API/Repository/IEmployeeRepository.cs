using BenefitsCalculation.API.Entities;

namespace BenefitsCalculation.API.Repository;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetEmployeesAsync();
    Task<Employee?> GetEmployeeAsync(int employeeId);
    Task<int> AddDependentAsync(int employeeId, Dependent dependent);
    void UpdateDependentAsync(Dependent dependent);
    void DeleteDependentAsync(int dependentId);
    Task<int> UpdateBenefitCost(int employeeId, double payPeriodBenefitCost);
}

