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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionChartsController : ControllerBase
    {
        private readonly HRContext _context;

        public InstitutionChartsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/InstitutionCharts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionChart>>> GetINSTITUTION_CHART()
        {
            return await _context.INSTITUTION_CHART.ToListAsync();
        }

        // GET: api/InstitutionCharts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionChart>> GetInstitutionChart(long id)
        {
            var institutionChart = await _context.INSTITUTION_CHART.FindAsync(id);

            if (institutionChart == null)
            {
                return NotFound();
            }

            return institutionChart;
        }

        // PUT: api/InstitutionCharts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitutionChart(long id, InstitutionChart institutionChart)
        {
            if (id != institutionChart.Id)
            {
                return BadRequest();
            }

            _context.Entry(institutionChart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitutionChartExists(id))
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

        // POST: api/InstitutionCharts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InstitutionChart>> PostInstitutionChart(InstitutionChart institutionChart)
        {
            _context.INSTITUTION_CHART.Add(institutionChart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitutionChart", new { id = institutionChart.Id }, institutionChart);
        }

        // DELETE: api/InstitutionCharts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InstitutionChart>> DeleteInstitutionChart(long id)
        {
            var institutionChart = await _context.INSTITUTION_CHART.FindAsync(id);
            if (institutionChart == null)
            {
                return NotFound();
            }

            _context.INSTITUTION_CHART.Remove(institutionChart);
            await _context.SaveChangesAsync();

            return institutionChart;
        }

        private bool InstitutionChartExists(long id)
        {
            return _context.INSTITUTION_CHART.Any(e => e.Id == id);
        }
    }
}
