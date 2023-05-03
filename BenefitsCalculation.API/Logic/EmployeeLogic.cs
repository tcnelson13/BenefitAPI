using BenefitsCalculation.API.Repository;
using BenefitsCalculation.API.Entities;
using BenefitsCalculation.API.Models;

namespace BenefitsCalculation.API.Logic;

public class EmployeeLogic : IEmployeeLogic
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPayrollAdminRepository _payrollAdminRepository;

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
}
