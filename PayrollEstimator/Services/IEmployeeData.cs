using System.Collections.Generic;

namespace PayrollEstimator.Services
{
    /// <summary>
    /// Interface for all updateable properties for a Employee
    /// </summary>
    public interface IEmployeeData
    {
        string FirstName { get; }
        string LastName { get; }
        List<IDependentData> Dependents { get; }
    }
}
