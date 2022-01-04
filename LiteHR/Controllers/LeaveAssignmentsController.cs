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
    public class LeaveAssignmentsController : ControllerBase
    {
        private readonly HRContext _context;

        public LeaveAssignmentsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/LeaveAssignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveAssignment>>> GetLEAVE_ASSIGNMENT()
        {
            return await _context.LEAVE_ASSIGNMENT.Include(a => a.Leave).Include(a => a.Rank).ToListAsync();
        }

        // GET: api/LeaveAssignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAssignment>> GetLeaveAssignment(long id)
        {
            var leaveAssignment = await _context.LEAVE_ASSIGNMENT.Include(a => a.Rank).Include(a => a.Leave).Where(a => a.Id == id).FirstOrDefaultAsync();

            if (leaveAssignment == null)
            {
                return NotFound();
            }

            return leaveAssignment;
        }

        // PUT: api/LeaveAssignments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveAssignment(long id, LeaveAssignment leaveAssignment)
        {
            if (id != leaveAssignment.Id)
            {
                return BadRequest();
            }

            _context.Entry(leaveAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveAssignmentExists(id))
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

        // POST: api/LeaveAssignments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LeaveAssignment>> PostLeaveAssignment(LeaveAssignment leaveAssignment)
        {
            _context.LEAVE_ASSIGNMENT.Add(leaveAssignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaveAssignment", new { id = leaveAssignment.Id }, leaveAssignment);
        }

        // DELETE: api/LeaveAssignments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LeaveAssignment>> DeleteLeaveAssignment(long id)
        {
            var leaveAssignment = await _context.LEAVE_ASSIGNMENT.FindAsync(id);
            if (leaveAssignment == null)
            {
                return NotFound();
            }

            _context.LEAVE_ASSIGNMENT.Remove(leaveAssignment);
            await _context.SaveChangesAsync();

            return leaveAssignment;
        }

        private bool LeaveAssignmentExists(long id)
        {
            return _context.LEAVE_ASSIGNMENT.Any(e => e.Id == id);
        }
    }
}
