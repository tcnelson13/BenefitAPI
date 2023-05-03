using BenefitsCalculation.API.Models;

namespace BenefitsCalculation.API.Logic;

public interface IEmployeeLogic
{
    Task<EmployeeModel?> GetEmployeeAsync(int employeeId);
    Task<List<EmployeeModel>> GetEmployeesAsync();
    Task<int> AddDependentAsync(int employeeId, DependentModel dependentModel);
}
