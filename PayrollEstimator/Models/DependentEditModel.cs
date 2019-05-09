using PayrollEstimator.Services;

namespace PayrollEstimator.Models
{
    public class DependentEditModel : IDependentData
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
    }
}