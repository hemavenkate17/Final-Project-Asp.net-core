using System.Collections.Generic;
using System.Threading.Tasks;
using WFM_WebAPI.Models;

namespace WFM_WebAPI.Repo
{
    public interface ISkillsService
    {
        Task<IEnumerable<Skills_Employees>> GetAllSkills();
    }
}
