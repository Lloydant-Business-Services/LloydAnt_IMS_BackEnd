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
    public class GradeBenefitController : ControllerBase
    {
        private readonly HRContext _context;

        public GradeBenefitController(HRContext context)
        {
            _context = context;
        }

        // GET: api/GradeBenefit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeBenefit>>> GetGRADEBENEFITS()
        {
            return await _context.GRADE_BENEFIT.ToListAsync();
        }

        // GET: api/GradeBenefit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeBenefit>> GetGradeBenefit(long id)
        {
            var gradeBenefit = await _context.GRADE_BENEFIT.FindAsync(id);

            if (gradeBenefit == null)
            {
                return NotFound();
            }

            return gradeBenefit;
        }

        // POST: api/GradeBenefit
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<GradeBenefit>> PostGradeBenefit(GradeBenefit gradeBenefit)
        {
            _context.GRADE_BENEFIT.Add(gradeBenefit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGradeBenefit", new { id = gradeBenefit.Id }, gradeBenefit);
        }

        // DELETE: api/GradeBenefit/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GradeBenefit>> DeleteGradeBenefit(long id)
        {
            var gradeBenefit = await _context.GRADE_BENEFIT.FindAsync(id);
            if (gradeBenefit == null)
            {
                return NotFound();
            }

            _context.GRADE_BENEFIT.Remove(gradeBenefit);
            await _context.SaveChangesAsync();

            return gradeBenefit;
        }

        // PUT: api/GradeBenefit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalaryStep(long id, GradeBenefit gradeBenefit)
        {
            if (id != gradeBenefit.Id)
            {
                return BadRequest();
            }

            _context.Entry(gradeBenefit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeBenefitExists(id))
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

        private bool GradeBenefitExists(long id)
        {
            return _context.GRADE_BENEFIT.Any(e => e.Id == id);
        }
    }
}