using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryTypeController : Controller
    {
        private readonly HRContext _context;

        public SalaryTypeController(HRContext context)
        {
            _context = context;
        }

        // GET: api/SalaryTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryType>>> GetSALARY_TYPES()
        {
            return await _context.SALARY_TYPE.ToListAsync();
        }

        // GET: api/SalaryType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryType>> GetSalaryType(long id)
        {
            var salaryType = await _context.SALARY_TYPE.FindAsync(id);

            if (salaryType == null)
            {
                return NotFound();
            }

            return salaryType;
        }

        // POST: api/SalaryType
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SalaryType>> PostSalaryType(SalaryType salaryType)
        {
            _context.SALARY_TYPE.Add(salaryType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalaryType", new { id = salaryType.Id }, salaryType);
        }

        // DELETE: api/SalaryType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SalaryType>> DeleteSalaryType(long id)
        {
            var salaryType = await _context.SALARY_TYPE.FindAsync(id);
            if (salaryType == null)
            {
                return NotFound();
            }

            _context.SALARY_TYPE.Remove(salaryType);
            await _context.SaveChangesAsync();

            return salaryType;
        }

        // PUT: api/SalaryType/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalaryType(long id, SalaryType salaryType)
        {
            if (id != salaryType.Id)
            {
                return BadRequest();
            }

            _context.Entry(salaryType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException DBex)
            {
                if (!SalaryTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw DBex;
                }
            }
            catch(Exception ex) { throw ex; }

            return NoContent();
        }

        private bool SalaryTypeExists(long id)
        {
            return _context.SALARY_TYPE.Any(e => e.Id == id);
        }
    }
}