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
    public class SalaryExtraTypeController : Controller
    {
        private readonly HRContext _context;

        public SalaryExtraTypeController(HRContext context)
        {
            _context = context;
        }

        // GET: api/SalaryExtraTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryExtraType>>> GetSALARY_EXTRA_TYPES()
        {
            return await _context.SALARY_EXTRA_TYPE.ToListAsync();
        }

        // GET: api/SalaryExtraType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryExtraType>> GetSalaryExtraType(long id)
        {
            var salaryExtraType = await _context.SALARY_EXTRA_TYPE.FindAsync(id);

            if (salaryExtraType == null)
            {
                return NotFound();
            }

            return salaryExtraType;
        }

        // POST: api/SalaryExtraType
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SalaryExtraType>> PostSalaryExtraType(SalaryExtraType salaryExtraType)
        {
            SalaryExtraType salaryExtraTypeExists = await _context.SALARY_EXTRA_TYPE.Where(s => s.Name == salaryExtraType.Name).FirstOrDefaultAsync();
            if (salaryExtraTypeExists != null)
            {
                return BadRequest(new { Message = "Salary extra type already exits" });
            }

            _context.SALARY_EXTRA_TYPE.Add(salaryExtraType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalaryExtraType", new { id = salaryExtraType.Id }, salaryExtraType);
        }

        // DELETE: api/SalaryExtraType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SalaryExtraType>> DeleteSalaryExtraType(long id)
        {
            var salaryExtraType = await _context.SALARY_EXTRA_TYPE.FindAsync(id);
            if (salaryExtraType == null)
            {
                return NotFound();
            }

            _context.SALARY_EXTRA_TYPE.Remove(salaryExtraType);
            await _context.SaveChangesAsync();

            return salaryExtraType;
        }

        // PUT: api/SalaryType/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalaryExtraType(long id, SalaryExtraType salaryExtraType)
        {
            if (id != salaryExtraType.Id)
            {
                return BadRequest();
            }

            _context.Entry(salaryExtraType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaryExtraTypeExists(id))
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

        private bool SalaryExtraTypeExists(long id)
        {
            return _context.SALARY_EXTRA_TYPE.Any(e => e.Id == id);
        }
    }
}