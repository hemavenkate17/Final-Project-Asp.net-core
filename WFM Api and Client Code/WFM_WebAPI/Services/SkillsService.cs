using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WFM_WebAPI.Models;
using WFM_WebAPI.Repo;

namespace WFM_WebAPI.Services
{
    public class SkillsService : ISkillsService
    {
        private readonly SQL_DBContext _DbContext;
        public SkillsService(SQL_DBContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<IEnumerable<Skills_Employees>> GetAllSkills()
        {
            var result = await _DbContext.Skills.Include(x => x.Skillmaps).ThenInclude(x => x.Skills).Select(x => new Skills_Employees
            {
                skillid = x.skillid,
                name = x.name,
                Employees = x.Skillmaps.Select(y => y.Employees.name).ToList()
            }).ToListAsync();
            return result;
        }
    }
}
