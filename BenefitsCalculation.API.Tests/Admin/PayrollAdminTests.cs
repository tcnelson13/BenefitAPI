using System;
using System.Collections.Generic;
using BenefitsCalculation.API.Entities;
using BenefitsCalculation.API.Logic;
using BenefitsCalculation.API.Repository;

namespace BenefitsCalculation.API.Tests.Admin
{
    public class PayrollAdminTests
    {
        [Fact]
        public void PreviewPayroll()
        {
            // Arrange
            var employeeModels = EmployeesDataStore.Current.Employees;
            var payrollAdminLogic = new PayrollAdminLogic(new PayrollAdminRepository(),
                new EmployeeLogic(new EmployeeRepository(), new PayrollAdminRepository()));

            List<BenefitRate> benefitRates = new List<BenefitRate>()
                {
                new BenefitRate()
                {
                    BenefitRateId = 1,
                    Name = "Employee",
                    AnnualBenefitCost = 1500
                },
                new BenefitRate()
                {
                    BenefitRateId = 2,
                    Name = "Dependent",
                    AnnualBenefitCost = 3000
                },
            };

            // Act
            foreach (var employeeModel in employeeModels)
            {
                employeeModel.PayPeriodBenefitCost = payrollAdminLogic.CalculateTotalBenefitCost(employeeModel, benefitRates);
            }

            var employeeCost1 = employeeModels[0].PayPeriodBenefitCost;
            var employeeCost2 = employeeModels[2].PayPeriodBenefitCost;

            // Assert
            Assert.NotEmpty(employeeModels);
            Assert.Equal(1500, employeeCost1);
            Assert.Equal(4350, employeeCost2);
        }
    }
}
