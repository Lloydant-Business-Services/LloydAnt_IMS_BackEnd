using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{
   
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationSectionHeaderController : ControllerBase
    {
        private readonly HRContext _context;

        public ApplicationSectionHeaderController(HRContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationSectionHeader
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationSectionHeader>>> GetAPPLICATIONSECTION()
        {
            return await _context.APPLICATION_SECTION_HEADER.ToListAsync();
        }

        // GET: api/ApplicationSectionHeader/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationSectionHeader>> GetApplicationSectionHeader(long id)
        {
            var ApplicationSectionHeader = await _context.APPLICATION_SECTION_HEADER.FindAsync(id);

            if (ApplicationSectionHeader == null)
            {
                return NotFound();
            }

            return ApplicationSectionHeader;
        }

        // PUT: api/JobVacancy/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationSectionHeader(long id, ApplicationSectionHeader applicationSectionHeader)
        {
            if (id != applicationSectionHeader.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationSectionHeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationSectionHeaderExists(id))
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

        // POST: api/ApplicationSectionHeader
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ApplicationSectionHeader>> PostApplicationSectionHeader(ApplicationSectionHeader applicationSectionHeader)
        {
            _context.APPLICATION_SECTION_HEADER.Add(applicationSectionHeader);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationSectionHeader", new { id = applicationSectionHeader.Id }, applicationSectionHeader);
        }

        // DELETE: api/ApplicationSectionHeader/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplicationSectionHeader>> DeleteApplicationSectionHeader(long id)
        {
            var applicationSectionHeader = await _context.APPLICATION_SECTION_HEADER.FindAsync(id);
            if (applicationSectionHeader == null)
            {
                return NotFound();
            }

            _context.APPLICATION_SECTION_HEADER.Remove(applicationSectionHeader);
            await _context.SaveChangesAsync();

            return applicationSectionHeader;
        }

        private bool ApplicationSectionHeaderExists(long id)
        {
            return _context.APPLICATION_SECTION_HEADER.Any(e => e.Id == id);
        }
    }
}