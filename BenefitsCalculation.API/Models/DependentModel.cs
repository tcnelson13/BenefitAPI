using BenefitsCalculation.API.Entities;

namespace BenefitsCalculation.API.Models;

public class DependentModel
{
    public long DependentId { get; set; }
    public long EmployeeId { get; set; }
    public string? Name { get; set; }
    public DependentType DependentTypeId { get; set; }
    public BenefitRateType BenefitRateId { get; set; }

    public static List<DependentModel> FromDependents(List<Dependent> dependents)
    {
        List<DependentModel> dependentModels = new();

        foreach (var dependent in dependents)
        {
            dependentModels.Add(new DependentModel
            {
                DependentId = dependent.DependentId,
                EmployeeId = dependent.EmployeeId,
                Name = dependent.Name,
                DependentTypeId = dependent.DependentTypeId == 1 ? DependentType.Spouse : DependentType.Child,
                BenefitRateId = dependent.BenefitRateId == 1 ? BenefitRateType.Employee : BenefitRateType.Dependent,
            });
        }
        return dependentModels; 
    }

    public static List<Dependent> ToDependents(List<DependentModel> dependentModels)
    {
        List<Dependent> dependents = new();
        foreach (var dependentModel in dependentModels)
        {
            dependents.Add(ToDependent(dependentModel));
        }
        return dependents;
    }

    public static Dependent ToDependent(DependentModel dependentModel)
    {
        return (new Dependent
        {
            DependentId = dependentModel.DependentId,
            EmployeeId = dependentModel.EmployeeId,
            Name = dependentModel.Name,
            DependentTypeId = dependentModel.DependentTypeId == DependentType.Spouse ? 1 : 2,
            BenefitRateId = dependentModel.BenefitRateId == BenefitRateType.Employee ? 1 : 2,
        });
    }
}

