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
    public class SalaryStepController : ControllerBase
    {
        private readonly HRContext _context;

        public SalaryStepController(HRContext context)
        {
            _context = context;
        }

        // GET: api/SalarySteps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaryStep>>> GetSALARY_STEPS()
        {
            return await _context.SALARY_STEP.ToListAsync();
        }

        // GET: api/SalaryStep/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalaryStep>> GetSalaryStep(long id)
        {
            var salaryStep = await _context.SALARY_STEP.FindAsync(id);

            if (salaryStep == null)
            {
                return NotFound();
            }

            return salaryStep;
        }

        // POST: api/SalaryStep
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<int>> PostSalaryStep(SalaryStep salaryStep)
        {
            _context.SALARY_STEP.Add(salaryStep);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // DELETE: api/SalaryStep/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteSalaryStep(long id)
        {
            var salaryStep = await _context.SALARY_STEP.FindAsync(id);
            if (salaryStep == null)
            {
                return NotFound();
            }

            _context.SALARY_STEP.Remove(salaryStep);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // PUT: api/SalaryStep/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutSalaryStep(long id, SalaryStep salaryStep)
        {
            if (id != salaryStep.Id)
            {
                return BadRequest();
            }

            _context.SALARY_STEP.Update(salaryStep);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        private bool SalaryLevelExists(long id)
        {
            return _context.SALARY_LEVEL.Any(e => e.Id == id);
        }
    }
}