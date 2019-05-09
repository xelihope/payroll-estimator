using System.Data.Entity;
using System.Threading.Tasks;
using PayrollEstimator.Data.Entities;

namespace PayrollEstimator.Data.Context
{
    public interface IPayrollEstimatorContext
    {
        DbSet<Employee> Employees { get; set; }

        DbSet<Dependent> Dependents { get; set; }

        Task<int> SaveChangesAsync();
    }
}
