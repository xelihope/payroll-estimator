using System.Collections.Generic;
using System.Data.Entity;
using PayrollEstimator.Data.Context;
using PayrollEstimator.Data.Entities;

namespace PayrollEstimator.Data.Seed
{
    /// <summary>
    /// Initializer of Payroll Estimator test data
    /// </summary>
    public class PayrollEstimatorDbInitializer : DropCreateDatabaseAlways<PayrollEstimatorContext>
    {
        protected override void Seed(PayrollEstimatorContext context)
        {
            var employees = new List<Employee> {
                new Employee {
                    EmployeeId = 1, FirstName = "Elizabeth", LastName = "Gorden",
                    Dependents = new List<Dependent> {
                        new Dependent { DependentId = 1, FirstName = "Dependent", LastName = "DependentLast"},
                        new Dependent { DependentId = 2, FirstName = "Anduin", LastName = "Wrynn" }
                    }
                },
                new Employee { EmployeeId = 2, FirstName = "Aaron", LastName = "Van de Merkt" },
                new Employee { EmployeeId = 3, FirstName = "Eric", LastName = "Bailey" },
                new Employee { EmployeeId = 4, FirstName = "Larry", LastName = "Smith" },
                new Employee { EmployeeId = 5, FirstName = "Bill", LastName = "Bob" },
                new Employee { EmployeeId = 6, FirstName = "Alice", LastName = "Smith" },
                new Employee { EmployeeId = 7, FirstName = "Gert", LastName = "Gole" },
                new Employee { EmployeeId = 8, FirstName = "Billy", LastName = "Bobby" },
                new Employee { EmployeeId = 9, FirstName = "Ahmed", LastName = "Saharaz" },
                new Employee { EmployeeId = 10, FirstName = "Jaina", LastName = "Proudmoore" }
            };

            context.Employees.AddRange(employees);

            context.SaveChanges();
        }
    }
}