using System;
using System.Linq;
using PayrollEstimator.Data.Entities;

namespace PayrollEstimator.Services
{
    public class EmployeeCostCalculator
    {
        /// <summary>
        /// Calculate payroll data for the input employee
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="paychecksPerYear"></param>
        /// <returns></returns>
        public EmployeeCostPerPaycheckDto Calculate(Employee employee, int paychecksPerYear = 26)
        {
            var employeeCost = new EmployeeCostPerPaycheckDto { PaychecksPerYear = paychecksPerYear };

            // Employees whose name starts with 'a' get a 10% discount
            if (employee.FirstName.StartsWith("a", StringComparison.InvariantCultureIgnoreCase)) {
                employeeCost.BenefitsDiscount = .1m;
            }
            employeeCost.BenefitsDeduction = 1000 * (1 - employeeCost.BenefitsDiscount) / paychecksPerYear;
            employeeCost.Pay = 2000m * 26 / paychecksPerYear;

            foreach (var dependent in employee.Dependents) {
                var dependentCost = new DependentCostPerPaycheckDto();
                // Dependents whose name starts with 'a' get a 10% discount
                if (dependent.FirstName.StartsWith("a", StringComparison.InvariantCultureIgnoreCase)) {
                    dependentCost.BenefitsDiscount = .1m;
                }
                dependentCost.BenefitsDeduction = 500 * (1 - dependentCost.BenefitsDiscount) / paychecksPerYear;
                employeeCost.DependentsCost[dependent.DependentId] = dependentCost;
            }
            employeeCost.DependentBenefitsDeduction =
                employeeCost.DependentsCost.Sum(kvp => kvp.Value.BenefitsDeduction);

            employeeCost.NetCost = employeeCost.Pay - employeeCost.BenefitsDeduction -
                                   employeeCost.DependentBenefitsDeduction;

            return employeeCost;
        }
    }
}