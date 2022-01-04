using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiteHR.Models;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationalQualificationsController : ControllerBase
    {
        private readonly HRContext _context;

        public EducationalQualificationsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/EducationalQualifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationalQualification>>> GetEDUCATIONAL_QUALIFICATION()
        {
            return await _context.EDUCATIONAL_QUALIFICATION.ToListAsync();
        }

        // GET: api/EducationalQualifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EducationalQualification>> GetEducationalQualification(long id)
        {
            var educationalQualification = await _context.EDUCATIONAL_QUALIFICATION.FindAsync(id);

            if (educationalQualification == null)
            {
                return NotFound();
            }

            return educationalQualification;
        }

        // PUT: api/EducationalQualifications/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<EducationalQualification>> PutEducationalQualification(long id, EducationalQualification educationalQualification)
        {
            if (id != educationalQualification.Id)
            {
                return BadRequest();
            }
            _context.EDUCATIONAL_QUALIFICATION.Update(educationalQualification);
            await _context.SaveChangesAsync();
            return educationalQualification;
            
        }

        // POST: api/EducationalQualifications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EducationalQualification>> PostEducationalQualification(EducationalQualification educationalQualification)
        {
            _context.EDUCATIONAL_QUALIFICATION.Add(educationalQualification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEducationalQualification", new { id = educationalQualification.Id }, educationalQualification);
        }

        // DELETE: api/EducationalQualifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteEducationalQualification(long id)
        {
            var educationalQualification = await _context.EDUCATIONAL_QUALIFICATION.FindAsync(id);
            if (educationalQualification == null)
            {
                return NotFound();
            }

            _context.EDUCATIONAL_QUALIFICATION.Remove(educationalQualification);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool EducationalQualificationExists(long id)
        {
            return _context.EDUCATIONAL_QUALIFICATION.Any(e => e.Id == id);
        }
    }
}
