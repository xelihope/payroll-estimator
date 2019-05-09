using System.ComponentModel.DataAnnotations;

namespace PayrollEstimator.Data.Entities
{
    public class Dependent
    {
        [Key]
        public int DependentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual Employee Dependee { get; set; }
    }
}