using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PayrollEstimator.Data.Context;
using PayrollEstimator.Data.Entities;

namespace PayrollEstimator.Services
{
    /// <summary>
    /// Create-Read-Update-Delete functionality around Employee objects and their common foreign children
    /// </summary>
    public class EmployeeCrud
    {
        private readonly IPayrollEstimatorContext _dbContext;

        public EmployeeCrud(IPayrollEstimatorContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Returns all Employee objects paged by the input page size and page number,
        /// filtered by whether or not the first or last name contains the name filter string.
        /// The output is paged by a first name ordering.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Employee>> Get()
        {
            return await _dbContext.Employees.Include(nameof(Employee.Dependents))
                .OrderBy(p => p.FirstName).ToListAsync();
        }

        /// <summary>
        /// Creates a new Employee object using input data
        /// </summary>
        /// <param name="data">Employee related data</param>
        /// <returns>The newly created Employee object</returns>
        public async Task<int> Create(IEmployeeData data)
        {
            var newEmployee = new Employee {
                FirstName = data.FirstName,
                LastName = data.LastName
            };
            if (data.Dependents != null) {
                newEmployee.Dependents = data.Dependents.Select(d => new Dependent {
                    FirstName = d.FirstName,
                    LastName = d.LastName
                }).ToList();
            }

            _dbContext.Employees.Add(newEmployee);

            await _dbContext.SaveChangesAsync();

            return newEmployee.EmployeeId;
        }
    }
}