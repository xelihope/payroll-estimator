using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PayrollEstimator.Data.Entities;

namespace PayrollEstimator.Data.Context
{
    public class PayrollEstimatorContext : DbContext, IPayrollEstimatorContext
    {
        public PayrollEstimatorContext() : base("PayrollEstimatorContext") { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Dependent> Dependents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}