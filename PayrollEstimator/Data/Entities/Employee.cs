using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollEstimator.Data.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }

        public Employee()
        {
            Dependents = new HashSet<Dependent>();
        }
    }
}