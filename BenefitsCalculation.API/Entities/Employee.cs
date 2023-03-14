using System;
using BenefitsCalculation.API.Models;

namespace BenefitsCalculation.API.Entities
{
	public class Employee
	{
        public long EmployeeId { get; set; }
        public string? Name { get; set; }
        public long AnnualSalary { get; set; }
        public long AnnualPaycheckCount { get; set; }
        public double PayPeriodBenefitCost { get; set; }
        public long BenefitRateId { get; set; }
        public List<Dependent> Dependents { get; set; } = new List<Dependent>();
    }
}

