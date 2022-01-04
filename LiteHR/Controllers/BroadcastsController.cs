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
    public class BroadcastsController : ControllerBase
    {
        private readonly HRContext _context;

        public BroadcastsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/Broadcasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Broadcast>>> GetBROADCAST()
        {
            return await _context.BROADCAST.ToListAsync();
        }

        // GET: api/Broadcasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Broadcast>> GetBroadcast(long id)
        {
            var broadcast = await _context.BROADCAST.FindAsync(id);

            if (broadcast == null)
            {
                return NotFound();
            }

            return broadcast;
        }

        // PUT: api/Broadcasts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("EditBroadcast")]
        public async Task<int> PutBroadcast(long id, Broadcast broadcast)
        {
        
            

            try
            {
                var _broadcast = await _context.BROADCAST.Where(d => d.Id == id).FirstOrDefaultAsync();
                _broadcast.Subject = broadcast.Subject;
                _broadcast.Details = broadcast.Details;
                _broadcast.Date = broadcast.Date;
                _broadcast.RankId = 4;
                _context.Update(_broadcast);
                await _context.SaveChangesAsync();
                return 200;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BroadcastExists(id))
                {
                    return 0;
                }
                else
                {
                    throw;
                }
            }

          
        }

        // POST: api/Broadcasts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Broadcast>> PostBroadcast(Broadcast broadcast)
        {
            broadcast.Date = DateTime.Now;
            _context.BROADCAST.Add(broadcast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBroadcast", new { id = broadcast.Id }, broadcast);
        }

        // DELETE: api/Broadcasts/5
        [HttpPost("DeleteBroadcast")]
        public async Task<int> DeleteBroadcast(long id)
        {
            var broadcast = await _context.BROADCAST.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (broadcast == null)
            {
                return 0;
            }

            _context.BROADCAST.Remove(broadcast);
            await _context.SaveChangesAsync();

            return 200;
        }

        private bool BroadcastExists(long id)
        {
            return _context.BROADCAST.Any(e => e.Id == id);
        }
    }
}
