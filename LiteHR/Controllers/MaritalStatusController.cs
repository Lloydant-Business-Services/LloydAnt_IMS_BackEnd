using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;

namespace LiteHR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaritalStatusController : ControllerBase
    {
        private readonly HRContext _context;

        public MaritalStatusController(HRContext context)
        {
            _context = context;
        }

        // GET: api/MaritalStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaritalStatus>>> GetMARITAL_STATUS()
        {
            return await _context.MARITAL_STATUS.ToListAsync();
        }

        // GET: api/MaritalStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaritalStatus>> GetMaritalStatus(long id)
        {
            var maritalStatus = await _context.MARITAL_STATUS.FindAsync(id);

            if (maritalStatus == null)
            {
                return NotFound();
            }

            return maritalStatus;
        }

        // PUT: api/MaritalStatus/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaritalStatus(long id, MaritalStatus maritalStatus)
        {
            if (id != maritalStatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(maritalStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaritalStatusExists(id))
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

        // POST: api/MaritalStatus
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MaritalStatus>> PostMaritalStatus(MaritalStatus maritalStatus)
        {
            _context.MARITAL_STATUS.Add(maritalStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaritalStatus", new { id = maritalStatus.Id }, maritalStatus);
        }

        // DELETE: api/MaritalStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MaritalStatus>> DeleteMaritalStatus(long id)
        {
            var maritalStatus = await _context.MARITAL_STATUS.FindAsync(id);
            if (maritalStatus == null)
            {
                return NotFound();
            }

            _context.MARITAL_STATUS.Remove(maritalStatus);
            await _context.SaveChangesAsync();

            return maritalStatus;
        }

        private bool MaritalStatusExists(long id)
        {
            return _context.MARITAL_STATUS.Any(e => e.Id == id);
        }
    }
}
