using System.Collections.Generic;

namespace PayrollEstimator.Models
{
    public class EmployeesViewModel
    {
        public IList<EmployeeViewModel> Employees { get; set; }

        public EmployeeCostViewModel AnnualTotals { get; set; }

        public EmployeeCostViewModel BiWeeklyTotals { get; set; }
    }
}