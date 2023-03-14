using System;
using BenefitsCalculation.API.Models;

namespace BenefitsCalculation.API.Tests
{
	public class EmployeesDataStore
	{
		public List<EmployeeModel> Employees { get; set; }
		public static EmployeesDataStore Current { get; } = new EmployeesDataStore();

		public EmployeesDataStore()
		{
			// Init dummy data
			Employees = new List<EmployeeModel>()
			{
				new EmployeeModel()
				{
					EmployeeId = 1,
					Name = "John Wick",
					AnnualPaycheckCount = 26,
					AnnualSalary = 52000,
                    PayPeriodBenefitCost = 38.46
				},
				new EmployeeModel()
				{
					EmployeeId = 2,
					Name = "James Bond",
					AnnualPaycheckCount = 26,
					AnnualSalary = 52000,
                    PayPeriodBenefitCost = 96.15,
					Dependents = new List<DependentModel>()
					{
						new DependentModel()
						{
							DependentId = 1,
                            EmployeeId = 2,
                            Name = "Alice Bond",
							DependentTypeId = DependentType.Spouse
						},
						new DependentModel()
						{
							DependentId = 2,
                            EmployeeId = 2,
                            Name = "Billy Bond",
							DependentTypeId = DependentType.Child
						},
						new DependentModel()
						{
							DependentId = 3,
                            EmployeeId = 2,
                            Name = "Margaret Bond",
							DependentTypeId = DependentType.Child
						}
					}
				},
                new EmployeeModel()
                {
                    EmployeeId = 3,
                    Name = "Andrew Shannon",
                    AnnualPaycheckCount = 26,
                    AnnualSalary = 52000,
                    PayPeriodBenefitCost = 53.85,
                    Dependents = new List<DependentModel>()
                    {
                        new DependentModel()
                        {
                            DependentId = 4,
							EmployeeId = 3,
                            Name = "Margaret Bond",
                            DependentTypeId = DependentType.Child
                        }
                    }
                },
            };
		}
	}
}

