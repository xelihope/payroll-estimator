using System.Web.Mvc;

namespace PayrollEstimator.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}