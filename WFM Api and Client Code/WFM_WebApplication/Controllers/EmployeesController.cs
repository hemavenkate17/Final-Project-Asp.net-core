using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WFM_WebAPI.Helpers;
using WFM_WebAPI.Repo;

namespace WFM_WebApplication.Controllers
{

    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeeService;

        public EmployeesController(IEmployeesService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var result = await _employeeService.GetAllEmployees();
            return View(result);
        }
    }
    
}
