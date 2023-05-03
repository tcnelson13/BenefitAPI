using Microsoft.AspNetCore.Mvc;
using BenefitsCalculation.API.Models;
using BenefitsCalculation.API.Logic;

namespace BenefitsCalculation.API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeLogic _employeeLogic;
    private readonly IBenefitCalc _benefitCalc;

    public EmployeesController(IEmployeeLogic employeeLogic, IBenefitCalc benefitCalc)
    {
        _employeeLogic = employeeLogic;
        _benefitCalc = benefitCalc;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmployeeModel>>> GetEmployees()
    {
        var employees = await _employeeLogic.GetEmployeesAsync();

        return Ok(employees);
    }

    [HttpGet("{employeeId}")]
    public async Task<ActionResult<EmployeeModel>> GetEmployee(int employeeId)
    {
        var employee = await _employeeLogic.GetEmployeeAsync(employeeId);

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }

    // POST api/values
    [HttpPost("{employeeId}", Name ="AddDependent")]
    public async Task<ActionResult<DependentModel>> AddDependent(int employeeId, DependentModel dependentModel)
    {
        int rowsAffected = await _employeeLogic.AddDependentAsync(employeeId, dependentModel);
        int? recordUpdated = await _benefitCalc.UpdateBenefitCostAsync(employeeId);
        var employee = await _employeeLogic.GetEmployeeAsync(employeeId);

        return CreatedAtRoute("AddDependent", new { employeeId = employeeId, newDependent = employee.Dependents.Max(d => d.DependentId) });
    }

    // PUT api/values/5
    [HttpPut("{employeeId}/{dependentId}")]
    public void UpdateDependent(int dependentId, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{employeeId}/{dependentId}")]
    public void DeleteDependent(int dependentId)
    {
    }
}

