namespace PayrollEstimator.Models
{
    public class DependentViewModel
    {
        public string Name { get; set; }

        public DependentCostViewModel AnnualCost { get; set; }

        public DependentCostViewModel BiWeeklyCost { get; set; }
    }
}