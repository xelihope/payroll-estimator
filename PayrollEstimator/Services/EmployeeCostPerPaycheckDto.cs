using System.Collections.Generic;

namespace PayrollEstimator.Services
{
    public class EmployeeCostPerPaycheckDto
    {
        public decimal PaychecksPerYear { get; set; }

        public decimal Pay { get; set; }

        public decimal BenefitsDiscount { get; set; }

        public decimal BenefitsDeduction { get; set; }

        public Dictionary<int, DependentCostPerPaycheckDto> DependentsCost { get; set; }
        
        public decimal DependentBenefitsDeduction { get; set; }
        
        public decimal NetCost { get; set; }

        public EmployeeCostPerPaycheckDto()
        {
            DependentsCost = new Dictionary<int, DependentCostPerPaycheckDto>();
        }
    }
}