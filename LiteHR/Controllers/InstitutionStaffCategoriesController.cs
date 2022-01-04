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
    public class InstitutionStaffCategoriesController : ControllerBase
    {
        private readonly HRContext _context;

        public InstitutionStaffCategoriesController(HRContext context)
        {
            _context = context;
        }

        // GET: api/InstitutionStaffCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionStaffCategory>>> GetINSTITUTION_STAFF_CATEGORY()
        {
            return await _context.INSTITUTION_STAFF_CATEGORY.ToListAsync();
        }

        // GET: api/InstitutionStaffCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionStaffCategory>> GetInstitutionStaffCategory(long id)
        {
            var institutionStaffCategory = await _context.INSTITUTION_STAFF_CATEGORY.FindAsync(id);

            if (institutionStaffCategory == null)
            {
                return NotFound();
            }

            return institutionStaffCategory;
        }

        // PUT: api/InstitutionStaffCategories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<InstitutionStaffCategory>> PutInstitutionStaffCategory(long id, InstitutionStaffCategory institutionStaffCategory)
        {
            if (id != institutionStaffCategory.Id)
            {
                return BadRequest();
            }

            _context.INSTITUTION_STAFF_CATEGORY.Update(institutionStaffCategory);
            await _context.SaveChangesAsync();
            return institutionStaffCategory;
           
            
        }

        // POST: api/InstitutionStaffCategories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InstitutionStaffCategory>> PostInstitutionStaffCategory(InstitutionStaffCategory institutionStaffCategory)
        {
            _context.INSTITUTION_STAFF_CATEGORY.Add(institutionStaffCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitutionStaffCategory", new { id = institutionStaffCategory.Id }, institutionStaffCategory);
        }

        // DELETE: api/InstitutionStaffCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteInstitutionStaffCategory(long id)
        {
            var institutionStaffCategory = await _context.INSTITUTION_STAFF_CATEGORY.FindAsync(id);
            if (institutionStaffCategory == null)
            {
                return NotFound();
            }

            _context.INSTITUTION_STAFF_CATEGORY.Remove(institutionStaffCategory);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool InstitutionStaffCategoryExists(long id)
        {
            return _context.INSTITUTION_STAFF_CATEGORY.Any(e => e.Id == id);
        }
    }
}
