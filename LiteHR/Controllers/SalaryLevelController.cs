using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryLevelController : ControllerBase
    {
        private readonly HRContext _context;

        public SalaryLevelController(HRContext context)
        {
            _context = context;
        }

        // GET: api/SalaryLevels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryLevel>>> GetSALARY_LEVELS()
        {
            return await _context.SALARY_LEVEL.ToListAsync();
        }

        // GET: api/SalaryLevel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryLevel>> GetSalaryLevel(long id)
        {
            var salaryLevel = await _context.SALARY_LEVEL.FindAsync(id);

            if (salaryLevel == null)
            {
                return NotFound();
            }

            return salaryLevel;
        }

        // POST: api/SalaryLevel
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<int>> PostSalaryLevel(SalaryLevel salaryLevel)
        {
            _context.SALARY_LEVEL.Add(salaryLevel);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // DELETE: api/SalaryLevel/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteSalaryLevel(long id)
        {
            var salaryLevel = await _context.SALARY_LEVEL.FindAsync(id);
            if (salaryLevel == null)
            {
                return NotFound();
            }

            _context.SALARY_LEVEL.Remove(salaryLevel);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // PUT: api/SalaryLevel/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutSalaryLevel(long id, SalaryLevel salaryLevel)
        {
            if (id != salaryLevel.Id)
            {
                return BadRequest();
            }

            _context.SALARY_LEVEL.Update(salaryLevel);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        private bool SalaryLevelExists(long id)
        {
            return _context.SALARY_LEVEL.Any(e => e.Id == id);
        }
    }
}