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
    public class EventsController : ControllerBase
    {
        private readonly HRContext _context;

        public EventsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEVENT()
        {

            return await _context.EVENT.OrderBy(x =>x.Date).Where(f => f.Date >= DateTime.Now).ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(long id)
        {
            var @event = await _context.EVENT.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutEvent(long id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.EVENT.Update(@event);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;

           

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<int>> PostEvent(Event @event)
        {
            _context.EVENT.Add(@event);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteEvent(long id)
        {
            var @event = await _context.EVENT.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.EVENT.Remove(@event);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool EventExists(long id)
        {
            return _context.EVENT.Any(e => e.Id == id);
        }
    }
}
