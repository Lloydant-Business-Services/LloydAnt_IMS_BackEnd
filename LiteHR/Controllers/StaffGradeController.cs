using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffGradeController : Controller
    {
        private readonly HRContext _context;

        public StaffGradeController(HRContext context)
        {
            _context = context;
        }

        // GET: api/StaffGrade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffGrade>>> GetSTAFFGRADE()
        {
            return await _context.STAFF_GRADE.ToListAsync();
        }

        // GET: api/StaffGrade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffGrade>> GetStaffGrade(long id)
        {
            var staffGrade = await _context.STAFF_GRADE.FindAsync(id);

            if (staffGrade == null)
            {
                return NotFound();
            }

            return staffGrade;
        }
        [HttpGet("StaffId")]
        public async Task<ActionResult<IEnumerable<StaffGrade>>> GetAllStaffGradeByStaffId(long id)
        {
            var staffGrade = await _context.STAFF_GRADE.Where(f=>f.StaffId==id)
                .Include(f=>f.Staff)
                .Include(f=>f.SalaryGrade)
                .Include(f=>f.SalaryGrade.SalaryStep)
                .Include(f=>f.SalaryGrade.SalaryLevel)
                .Include(f=>f.SalaryGrade.SalaryGradeCategory)
                             
                .ToListAsync();

            if (staffGrade == null)
            {
                return NotFound();
            }

            return staffGrade;
        }

        // POST: api/StaffGrade
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<StaffGrade>> PostGradeBenefit(StaffGrade staffGrade)
        {
            _context.STAFF_GRADE.Add(staffGrade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaffGrade", new { id = staffGrade.Id }, staffGrade);
        }

        // DELETE: api/StaffGrade/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StaffGrade>> DeleteStaffGrade(long id)
        {
            var staffGrade = await _context.STAFF_GRADE.FindAsync(id);
            if (staffGrade == null)
            {
                return NotFound();
            }

            _context.STAFF_GRADE.Remove(staffGrade);
            await _context.SaveChangesAsync();

            return staffGrade;
        }

        // PUT: api/StaffGrade/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaffGrade(long id, StaffGrade staffGrade)
        {
            if (id != staffGrade.Id)
            {
                return BadRequest();
            }

            _context.Entry(staffGrade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffGradeExists(id))
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

        private bool StaffGradeExists(long id)
        {
            return _context.STAFF_GRADE.Any(e => e.Id == id);
        }
    }
}