using System;
namespace BenefitsCalculation.API.Entities;

public class BenefitRate
{
    public long BenefitRateId { get; set; }
    public string? Name { get; set; }
    public double AnnualBenefitCost { get; set; }
}

