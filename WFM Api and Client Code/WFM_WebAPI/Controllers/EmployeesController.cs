using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WFM_WebAPI.Helpers;
using WFM_WebAPI.Models;
using WFM_WebAPI.Repo;

namespace WFM_WebAPI.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly SQL_DBContext _context;
        private readonly IEmployeesService _employeeService;

        public EmployeesController(SQL_DBContext context,IEmployeesService employeeService)
        {
            _employeeService = employeeService;
            _context = context;
        }

        [Authorize]
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees_Skills>>> GetEmployees()
        {
            var result = await _context.Employees.Include(x => x.Skillmaps).ThenInclude(x => x.Skills).Select(x => new Employees_Skills
            {
                Employee_id = x.employee_id,
                Name = x.name,
                Status = x.status,
                Manager = x.manager,
                Wfm_manager = x.wfm_manager,
                Email = x.email,
                Experience = x.experience,
                Lockstatus = x.lockstatus,
                Skills = x.Skillmaps.Select(y => y.Skills.name).ToList()
            }).Where(s => s.Lockstatus.Contains("Not Requested")).ToListAsync();

            return result;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id,Employee employee)
        {
            if (id != employee.employee_id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                 await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.employee_id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.employee_id == id);
        }
    }
}
