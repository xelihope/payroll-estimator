using System.Collections.Generic;
using System.Linq;
using System.Web.ModelBinding;
using PayrollEstimator.Services;

namespace PayrollEstimator.Models
{
    public class EmployeeEditModel : IEmployeeData
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public List<DependentEditModel> Dependents { get; set; }

        [BindNever]
        List<IDependentData> IEmployeeData.Dependents => Dependents.Cast<IDependentData>().ToList();
    }
}