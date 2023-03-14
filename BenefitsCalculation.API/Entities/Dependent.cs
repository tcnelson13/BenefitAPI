using System;
using BenefitsCalculation.API.Models;

namespace BenefitsCalculation.API.Entities
{
	public class Dependent
	{
        public long DependentId { get; set; }
        public long EmployeeId { get; set; }
        public string? Name { get; set; }
        public long DependentTypeId { get; set; }
        public long BenefitRateId { get; set; }
    }
}

