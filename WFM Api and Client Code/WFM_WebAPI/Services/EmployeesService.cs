using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WFM_WebAPI.Models;
using WFM_WebAPI.Repo;

namespace WFM_WebAPI.Services
{
    public class EmployeesService : IEmployeesService
    {
        private readonly SQL_DBContext _DbContext;
        public EmployeesService(SQL_DBContext _context)
        {
            _DbContext = _context;
        }

        public async Task<IEnumerable<Employees_Skills>> GetAllEmployees()
        {
            var result = await _DbContext.Employees.Include(x => x.Skillmaps).ThenInclude(x => x.Skills).Select(x => new Employees_Skills
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
    }
}
