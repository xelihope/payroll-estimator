using System.Collections.Generic;

namespace PayrollEstimator.Models
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }

        public List<DependentViewModel> Dependents { get; set; }

        public EmployeeCostViewModel AnnualCost { get; set; }

        public EmployeeCostViewModel BiWeeklyCost { get; set; }
    }
}