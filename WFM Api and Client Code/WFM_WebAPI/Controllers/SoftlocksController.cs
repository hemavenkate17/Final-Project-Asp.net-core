using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WFM_WebAPI.Models;
using WFM_WebAPI.Repo;

namespace WFM_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftlocksController : ControllerBase
    {
        private readonly SQL_DBContext _context;
      

        public SoftlocksController(SQL_DBContext context)
        {
  
            _context = context;
        }


       
        // GET: api/Softlocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoftlockRequest>>> GetSoftlocks()
        {
            var result = await _context.Softlock.Select(x => new SoftlockRequest
            {
                employee_id = x.employee_id,
                manager = x.manager,
                reqdate = x.reqdate,
                requestmessage = x.requestmessage,
                status=x.status,
                lockid=x.lockid
            }).Where(s => s.status.Contains("awaiting_confirmation")).ToListAsync();

            return result;
        }

        // GET: api/Softlocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Softlock>> GetSoftlock(int id)
        {
            var softlock = await _context.Softlock.FindAsync(id);

            if (softlock == null)
            {
                return NotFound();
            }

            return softlock;
        }

        // PUT: api/Softlocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoftlock(int id, Softlock softlock)
        {
            if (id != softlock.lockid)
            {
                return BadRequest();
            }

            _context.Entry(softlock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoftlockExists(id))
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

        // POST: api/Softlocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Softlock>> PostSoftlock(Softlock softlock)
        {
           
            
            try
            {
                softlock.status = "awaiting_confirmation";
                _context.Softlock.Add(softlock);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }

            return softlock;
        }

        // DELETE: api/Softlocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoftlock(int id)
        {
            var softlock = await _context.Softlock.FindAsync(id);
            if (softlock == null)
            {
                return NotFound();
            }

            _context.Softlock.Remove(softlock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SoftlockExists(int id)
        {
            return _context.Softlock.Any(e => e.lockid == id);
        }
    }
}
