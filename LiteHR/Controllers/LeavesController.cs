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
    public class LeavesController : ControllerBase
    {
        private readonly HRContext _context;

        public LeavesController(HRContext context)
        {
            _context = context;
        }

        // GET: api/Leaves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leave>>> GetLEAVE()
        {
            return await _context.LEAVE.Include(a => a.StaffType).ToListAsync();
        }

        // GET: api/Leaves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Leave>> GetLeave(long id)
        {
            var leave = await _context.LEAVE.FindAsync(id);

            if (leave == null)
            {
                return NotFound();
            }

            return leave;
        }

        // PUT: api/Leaves/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> PutLeave(long id, Leave leave)
        {
            if (id != leave.Id)
            {
                return BadRequest();
            }

            _context.LEAVE.Update(leave);
            await _context.SaveChangesAsync();
            return StatusCodes.Status200OK;
        }

        // POST: api/Leaves
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Leave>> PostLeave(Leave leave)
        {
            _context.LEAVE.Add(leave);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeave", new { id = leave.Id }, leave);
        }

        // DELETE: api/Leaves/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteLeave(long id)
        {
            var leave = await _context.LEAVE.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }

            _context.LEAVE.Remove(leave);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool LeaveExists(long id)
        {
            return _context.LEAVE.Any(e => e.Id == id);
        }
    }
}
