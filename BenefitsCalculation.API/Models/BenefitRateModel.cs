using System;
using BenefitsCalculation.API.Entities;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BenefitsCalculation.API.Models;

public class BenefitRateModel
{
    public long BenefitRateId { get; set; }
    public string? Name { get; set; }
    public double AnnualBenefitCost { get; set; }

    public static BenefitRateModel FromBenefitRate(BenefitRate benefitRate)
    {
        return new BenefitRateModel
        {
            BenefitRateId = benefitRate.BenefitRateId,
            Name = benefitRate.Name,
            AnnualBenefitCost = benefitRate.AnnualBenefitCost,
        };

    }

    public static BenefitRate ToBenefitRate(BenefitRateModel benefitRateModel)
    {
        return (new BenefitRate
        {
            BenefitRateId = benefitRateModel.BenefitRateId,
            Name = benefitRateModel.Name,
            AnnualBenefitCost = benefitRateModel.AnnualBenefitCost
        });

    }
}
