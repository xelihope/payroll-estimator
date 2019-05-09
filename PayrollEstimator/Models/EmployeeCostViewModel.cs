using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayrollEstimator.Models
{
    public class EmployeeCostViewModel
    {
        public decimal Pay { get; set; }

        public decimal BenefitsDiscount { get; set; }

        public decimal BenefitsDeduction { get; set; }

        public decimal DependentsBenefitsDeduction { get; set; }

        public decimal NetCost { get; set; }
    }
}