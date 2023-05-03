using BenefitsCalculation.API.Entities;

namespace BenefitsCalculation.API.Models;

public class EmployeeModel
{
    public long EmployeeId { get; set; }
    public string? Name { get; set; }
    public long AnnualSalary { get; set; }
    public long AnnualPaycheckCount { get; set; }
    public double PayPeriodBenefitCost { get; set; }
    public BenefitRateType BenefitRateId { get; set; }
    public List<DependentModel> Dependents { get; set; } = new List<DependentModel>();

    public static EmployeeModel FromEmployee(Employee employee)
    {
        return new EmployeeModel
        {
            EmployeeId = employee.EmployeeId,
            Name = employee.Name,
            AnnualPaycheckCount = employee.AnnualPaycheckCount,
            AnnualSalary = employee.AnnualSalary,
            PayPeriodBenefitCost = Math.Round(employee.PayPeriodBenefitCost, 2),
            BenefitRateId = employee.BenefitRateId == 1 ? BenefitRateType.Employee : BenefitRateType.Dependent,
            Dependents = DependentModel.FromDependents(employee.Dependents),
        };
    }

    public Employee ToEmployee(EmployeeModel employeeModel)
    {
        return new Employee
        {
            EmployeeId = EmployeeId,
            Name = Name,
            AnnualPaycheckCount = AnnualPaycheckCount,
            AnnualSalary = AnnualSalary,
            PayPeriodBenefitCost = PayPeriodBenefitCost,
            BenefitRateId = employeeModel.BenefitRateId == BenefitRateType.Employee ? 1 : 2,
            Dependents = DependentModel.ToDependents(employeeModel.Dependents),
        };
    }
}