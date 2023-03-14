using System;
using System.Collections.Generic;
using System.Xml.Linq;
using BenefitsCalculation.API.Entities;
using BenefitsCalculation.API.Models;

namespace BenefitsCalculation.API.Logic;

public interface IPayrollAdminLogic
{
	Task<List<EmployeeModel>> PreviewPayrollAsync();
	Task<List<EmployeeModel>> ProcessPayrollAsync();
    Task<List<BenefitRateModel>> GetBenefitRatesAsync();
    Task<int> UpdateBenefitRateAsync(BenefitRateModel benefitRateModel);
}
