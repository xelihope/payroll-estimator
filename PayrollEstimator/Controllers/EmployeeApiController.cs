using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PayrollEstimator.Models;
using PayrollEstimator.Services;

namespace PayrollEstimator.Controllers
{
    public class EmployeeApiController : ApiController
    {
        private readonly EmployeeCrud _employeeCrud;
        private readonly EmployeeCostCalculator _costCalculator;
        private readonly EmployeeValidator _employeeValidator;

        // DI ideally uses interfaces for the minimum sake of super easy unit test mocking
        // Pretend these are interfaces *waves magic hands*
        public EmployeeApiController(EmployeeCrud employeeCrud, EmployeeCostCalculator costCalculator,
            EmployeeValidator employeeValidator)
        {
            _employeeCrud = employeeCrud;
            _costCalculator = costCalculator;
            _employeeValidator = employeeValidator;
        }

        [HttpGet]
        [Route("api/employees")]
        public async Task<IHttpActionResult> Get()
        {
            if (!ModelState.IsValid) {
                BadRequest();
            }

            var employees = await _employeeCrud.Get();
            var mappedEmployees = employees
                .Select(e => {
                    var annualCost = _costCalculator.Calculate(e, 1);
                    var biWeeklyCost = _costCalculator.Calculate(e);
                    return new EmployeeViewModel {
                        Name = e.FirstName + " " + e.LastName,
                        Dependents = e.Dependents.Select(d => new DependentViewModel {
                            Name = d.FirstName + " " + d.LastName,
                            AnnualCost = MapToViewModel(annualCost.DependentsCost[d.DependentId]),
                            BiWeeklyCost = MapToViewModel(biWeeklyCost.DependentsCost[d.DependentId])
                        }).ToList(),
                        AnnualCost = MapToViewModel(annualCost),
                        BiWeeklyCost = MapToViewModel(biWeeklyCost)
                    };
                });

            return Ok(mappedEmployees);
        }

        [NonAction]
        private EmployeeCostViewModel MapToViewModel(EmployeeCostPerPaycheckDto cost)
        {
            return new EmployeeCostViewModel {
                Pay = cost.Pay,
                BenefitsDiscount = cost.BenefitsDiscount,
                BenefitsDeduction = cost.BenefitsDeduction,
                DependentsBenefitsDeduction = cost.DependentBenefitsDeduction,
                NetCost = cost.NetCost
            };
        }

        [NonAction]
        private DependentCostViewModel MapToViewModel(DependentCostPerPaycheckDto cost)
        {
            return new DependentCostViewModel {
                BenefitsDiscount = cost.BenefitsDiscount,
                BenefitsDeduction = cost.BenefitsDeduction
            };
        }

        [HttpPut]
        [Route("api/employee")]
        public async Task<IHttpActionResult> Create(EmployeeEditModel newEmployee)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            var errors = _employeeValidator.GetErrors(newEmployee);
            if (errors.Any()) {
                return BadRequest(string.Join(" ", errors));
            }

            var newEmployeeId = await _employeeCrud.Create(newEmployee);

            return Ok(newEmployeeId);
        }
    }
}