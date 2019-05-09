using NUnit.Framework;
using PayrollEstimator.Data.Entities;
using PayrollEstimator.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollEstimator.Tests.Services
{
    [TestFixture]
    public class EmployeeCostCalculatorTests
    {
        private static IList<TestCaseData> _calculateTests = new List<TestCaseData> {
            new TestCaseData(CreateTestEmployee("Name", new List<string>()), CreateTestCost())
                    .SetName("Calculate: employee no dependents + no discount"),
            new TestCaseData(CreateTestEmployee("ame", new List<string>()), CreateTestCost(benefitsDiscount: .10m, 
                benefitsDeduction: 900m/26))
                    .SetName("Calculate: employee no dependents + discount (lowercase name)"),
            new TestCaseData(CreateTestEmployee("Ame", new List<string>()), CreateTestCost(benefitsDiscount: .10m, 
                benefitsDeduction: 900m/26))
                    .SetName("Calculate: employee no dependents + discount (uppercase name)"),
            new TestCaseData(CreateTestEmployee("Name", new List<string>{ "Name", "Name" }), 
                CreateTestCost(dependentsBenefitsDeduction: 1000m/26))
                    .SetName("Calculate: employee 2 dependents + no discount"),
            new TestCaseData(CreateTestEmployee("Name", new List<string>{ "ame", "Ame" }),
                CreateTestCost(dependentsBenefitsDeduction: 900m/26))
                    .SetName("Calculate: employee 2 dependents + 2 dependent discounts"),
            new TestCaseData(CreateTestEmployee("ame", new List<string>{ "Name", "Ame" }),
                CreateTestCost(benefitsDiscount: .10m, benefitsDeduction: 900m/26,
                    dependentsBenefitsDeduction: 950m/26))
                    .SetName("Calculate: mixed bag")
        };

        [TestCaseSource(nameof(_calculateTests))]
        public void CalculateTests(Employee employee, EmployeeCostPerPaycheckDto expected)
        {
            // Act
            var result = new EmployeeCostCalculator().Calculate(employee);

            // Assert
            Assert.AreEqual(Math.Round(expected.Pay, 5), Math.Round(result.Pay));
            Assert.AreEqual(Math.Round(expected.BenefitsDiscount, 5), Math.Round(result.BenefitsDiscount, 5));
            Assert.AreEqual(Math.Round(expected.BenefitsDeduction, 5), Math.Round(result.BenefitsDeduction, 5));
            Assert.AreEqual(Math.Round(expected.DependentBenefitsDeduction, 5), Math.Round(result.DependentBenefitsDeduction, 5));
            Assert.AreEqual(Math.Round(expected.NetCost, 5), Math.Round(result.NetCost, 5));
        }

        private static Employee CreateTestEmployee(string firstName, IList<string> dependentFirstNames)
        {
            var dependentIndex = 1;
            return new Employee {
                FirstName = firstName,
                Dependents = dependentFirstNames.Select(x => new Dependent {
                    DependentId = dependentIndex++,
                    FirstName = x
                }).ToList()
            };
        }

        private static EmployeeCostPerPaycheckDto CreateTestCost(decimal pay = 2000m, int payChecksPerYear = 26, 
            decimal benefitsDiscount = 0, decimal benefitsDeduction = 1000m/26, decimal dependentsBenefitsDeduction = 0)
        {
            return new EmployeeCostPerPaycheckDto {
                Pay = pay,
                PaychecksPerYear = payChecksPerYear,
                BenefitsDiscount = benefitsDiscount,
                BenefitsDeduction = benefitsDeduction,
                NetCost = pay - benefitsDeduction - dependentsBenefitsDeduction,
                DependentBenefitsDeduction = dependentsBenefitsDeduction
            };
        }
    }
}