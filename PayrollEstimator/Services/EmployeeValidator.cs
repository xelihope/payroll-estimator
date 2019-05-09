using System.Collections.Generic;
using System.Linq;

namespace PayrollEstimator.Services
{
    public class EmployeeValidator
    {
        /// <summary>
        /// Validates the input employee for save
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public IList<string> GetErrors(IEmployeeData employee)
        {
            var errors = new List<string>();

            if (employee.FirstName == null || employee.FirstName.Trim() == ""
                || employee.LastName == null || employee.LastName.Trim() == "") {
                errors.Add(Resources.Employee.NameRequiredError);
            }

            if (employee.Dependents != null && 
                employee.Dependents.Any(d => d.FirstName == null || d.FirstName.Trim() == "" 
                    || d.LastName == null || d.LastName.Trim() == "")) {
                errors.Add(Resources.Employee.DependentNameRequiredError);
            }

            return errors;
        }
    }
}