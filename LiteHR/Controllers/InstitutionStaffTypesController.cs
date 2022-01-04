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
    public class InstitutionStaffTypesController : ControllerBase
    {
        private readonly HRContext _context;

        public InstitutionStaffTypesController(HRContext context)
        {
            _context = context;
        }

        // GET: api/InstitutionStaffTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionStaffType>>> GetINSTITUTION_STAFF_TYPE()
        {
            return await _context.INSTITUTION_STAFF_TYPE.ToListAsync();
        }

        // GET: api/InstitutionStaffTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionStaffType>> GetInstitutionStaffType(long id)
        {
            var institutionStaffType = await _context.INSTITUTION_STAFF_TYPE.FindAsync(id);

            if (institutionStaffType == null)
            {
                return NotFound();
            }

            return institutionStaffType;
        }

        // PUT: api/InstitutionStaffTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutInstitutionStaffType(long id, InstitutionStaffType institutionStaffType)
        {
            if (id != institutionStaffType.Id)
            {
                return BadRequest();
            }

            _context.INSTITUTION_STAFF_TYPE.Update(institutionStaffType);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        // POST: api/InstitutionStaffTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<int>> PostInstitutionStaffType(InstitutionStaffType institutionStaffType)
        {
            _context.INSTITUTION_STAFF_TYPE.Add(institutionStaffType);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // DELETE: api/InstitutionStaffTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteInstitutionStaffType(long id)
        {
            var institutionStaffType = await _context.INSTITUTION_STAFF_TYPE.FindAsync(id);
            if (institutionStaffType == null)
            {
                return NotFound();
            }

            _context.INSTITUTION_STAFF_TYPE.Remove(institutionStaffType);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool InstitutionStaffTypeExists(long id)
        {
            return _context.INSTITUTION_STAFF_TYPE.Any(e => e.Id == id);
        }
    }
}
