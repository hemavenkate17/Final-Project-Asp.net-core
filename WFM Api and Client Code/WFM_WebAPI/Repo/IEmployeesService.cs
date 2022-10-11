using System.Collections.Generic;
using System.Threading.Tasks;
using WFM_WebAPI.Models;

namespace WFM_WebAPI.Repo
{
    public interface IEmployeesService
    {
        Task<IEnumerable<Employees_Skills>> GetAllEmployees();
    }
}
