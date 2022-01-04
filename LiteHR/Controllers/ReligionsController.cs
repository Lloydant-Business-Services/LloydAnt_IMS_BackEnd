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
    public class ReligionsController : ControllerBase
    {
        private readonly HRContext _context;

        public ReligionsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/Religions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Religion>>> GetRELIGION()
        {
            return await _context.RELIGION.ToListAsync();
        }

        // GET: api/Religions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Religion>> GetReligion(long id)
        {
            var religion = await _context.RELIGION.FindAsync(id);

            if (religion == null)
            {
                return NotFound();
            }

            return religion;
        }

        // PUT: api/Religions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReligion(long id, Religion religion)
        {
            if (id != religion.Id)
            {
                return BadRequest();
            }

            _context.Entry(religion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReligionExists(id))
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

        // POST: api/Religions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Religion>> PostReligion(Religion religion)
        {
            _context.RELIGION.Add(religion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReligion", new { id = religion.Id }, religion);
        }

        // DELETE: api/Religions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Religion>> DeleteReligion(long id)
        {
            var religion = await _context.RELIGION.FindAsync(id);
            if (religion == null)
            {
                return NotFound();
            }

            _context.RELIGION.Remove(religion);
            await _context.SaveChangesAsync();

            return religion;
        }

        private bool ReligionExists(long id)
        {
            return _context.RELIGION.Any(e => e.Id == id);
        }
    }
}
