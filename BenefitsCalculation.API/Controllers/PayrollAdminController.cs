using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenefitsCalculation.API.Models;
using BenefitsCalculation.API.Logic;
using Microsoft.AspNetCore.Mvc;

namespace BenefitsCalculation.API.Controllers;

// TODO: This controller needs authentication and authorization

[ApiController]
[Route("api/admin")]
public class PayrollAdminController : ControllerBase
{
    private readonly IPayrollAdminLogic _payrollAdminLogic;

    public PayrollAdminController(IPayrollAdminLogic payrollAdminLogic)
    {
        _payrollAdminLogic = payrollAdminLogic;
    }

    [HttpGet("benefitrates")]
    public async Task<ActionResult<BenefitRateModel>> GetBenefitRates()
    {
        var benefitRates = await _payrollAdminLogic.GetBenefitRatesAsync();
        return Ok(benefitRates);
    }


    [HttpPut("{adminId}/{benefitRateTypeId}")]
    public async Task<ActionResult> UpdateBenefitRateAsync(BenefitRateModel benefitRateModel)
    {
        int rowsAffected = await _payrollAdminLogic.UpdateBenefitRateAsync(benefitRateModel);
        return Ok(rowsAffected);
    }

    [HttpGet("{adminId}/previewpayroll")]
    public async Task<ActionResult<List<EmployeeModel>>> PreviewPayrollAsync()
    {
        var employeeModels = await _payrollAdminLogic.PreviewPayrollAsync();
        return Ok(employeeModels);
    }

    [HttpPost("{adminId}/processpayroll", Name ="ProcessPayroll")]
    public async Task<ActionResult<List<EmployeeModel>>> ProcessPayrollAsync()
    {
        var employeeModels = await _payrollAdminLogic.ProcessPayrollAsync();
        return Ok(employeeModels);
    }
}

