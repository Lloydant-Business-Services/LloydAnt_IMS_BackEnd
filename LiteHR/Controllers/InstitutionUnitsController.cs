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
    public class InstitutionUnitsController : ControllerBase
    {
        private readonly HRContext _context;

        public InstitutionUnitsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/InstitutionUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionUnit>>> GetINSTITUTION_UNIT()
        {
            return await _context.INSTITUTION_UNIT
                .OrderBy(u => u.Name)
                .ToListAsync();
        }

        // GET: api/InstitutionUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionUnit>> GetInstitutionUnit(long id)
        {
            var institutionUnit = await _context.INSTITUTION_UNIT.FindAsync(id);

            if (institutionUnit == null)
            {
                return NotFound();
            }

            return institutionUnit;
        }

        // PUT: api/InstitutionUnits/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<InstitutionUnit>> PutInstitutionUnit(long id, InstitutionUnit institutionUnit)
        {
            if (id != institutionUnit.Id)
            {
                return BadRequest();
            }

            _context.INSTITUTION_UNIT.Update(institutionUnit);
            await _context.SaveChangesAsync();
            return institutionUnit;
        }

        // POST: api/InstitutionUnits
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InstitutionUnit>> PostInstitutionUnit(InstitutionUnit institutionUnit)
        {
            _context.INSTITUTION_UNIT.Add(institutionUnit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitutionUnit", new { id = institutionUnit.Id }, institutionUnit);
        }

        // DELETE: api/InstitutionUnits/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteInstitutionUnit(long id)
        {
            var institutionUnit = await _context.INSTITUTION_UNIT.FindAsync(id);
            if (institutionUnit == null)
            {
                return NotFound();
            }

            _context.INSTITUTION_UNIT.Remove(institutionUnit);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool InstitutionUnitExists(long id)
        {
            return _context.INSTITUTION_UNIT.Any(e => e.Id == id);
        }
    }
}
