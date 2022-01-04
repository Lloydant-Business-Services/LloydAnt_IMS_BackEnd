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
    public class LgasController : ControllerBase
    {
        private readonly HRContext _context;

        public LgasController(HRContext context)
        {
            _context = context;
        }

        // GET: api/Lgas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lga>>> GetLGA()
        {
            return await _context.LGA.ToListAsync();
        }

        // GET: api/Lgas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lga>> GetLga(long id)
        {
            var lga = await _context.LGA.FindAsync(id);

            if (lga == null)
            {
                return NotFound();
            }

            return lga;
        }
        [HttpGet("/api/Lgas/byStateId")]
        
        public async Task<ActionResult<IEnumerable<Lga>>> GetLgas(long id)
        {
            var lga = await _context.LGA.Where(m=>m.StateId==id && m.Active).ToListAsync();

            if (lga == null)
            {
                return NotFound();
            }

            return lga;
        }
        // PUT: api/Lgas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLga(long id, Lga lga)
        {
            if (id != lga.Id)
            {
                return BadRequest();
            }

            _context.Entry(lga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LgaExists(id))
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

        // POST: api/Lgas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Lga>> PostLga(Lga lga)
        {
            _context.LGA.Add(lga);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLga", new { id = lga.Id }, lga);
        }

        // DELETE: api/Lgas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lga>> DeleteLga(long id)
        {
            var lga = await _context.LGA.FindAsync(id);
            if (lga == null)
            {
                return NotFound();
            }

            _context.LGA.Remove(lga);
            await _context.SaveChangesAsync();

            return lga;
        }

        private bool LgaExists(long id)
        {
            return _context.LGA.Any(e => e.Id == id);
        }
    }
}
