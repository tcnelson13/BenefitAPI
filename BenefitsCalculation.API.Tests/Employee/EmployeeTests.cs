using System;
using BenefitsCalculation.API.Logic;
using BenefitsCalculation.API.Repository;

namespace BenefitsCalculation.API.Tests.Employee
{
	public class EmployeeTests
	{
        [Fact]
        public void CheckEmployeeRate()
        {
            // Arrange
            var employeeLogic = new EmployeeLogic(new EmployeeRepository(), new PayrollAdminRepository());

            // Act
            double standardRate = employeeLogic.CalculateDiscount("Joe Smithe", 1000);
            double discountRate = employeeLogic.CalculateDiscount("Andrew Jones", 1000);

            // Assert
            Assert.Equal(1000, standardRate);
            Assert.Equal(900, discountRate);
        }
    }
}

