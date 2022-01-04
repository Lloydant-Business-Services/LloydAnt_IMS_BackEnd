using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LiteHR.Models;
//using Microsoft.AspNetCore.Authorization;

namespace LiteHR.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionAppointmentsController : ControllerBase
    {
        private readonly HRContext _context;

        public InstitutionAppointmentsController(HRContext context)
        {
            _context = context;
        }

        // GET: api/InstitutionAppointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionAppointment>>> GetINSTITUTION_APPOINTMENT()
        {
            return await _context.INSTITUTION_APPOINTMENT.ToListAsync();
        }

        // GET: api/InstitutionAppointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionAppointment>> GetInstitutionAppointment(long id)
        {
            var institutionAppointment = await _context.INSTITUTION_APPOINTMENT.FindAsync(id);

            if (institutionAppointment == null)
            {
                return NotFound();
            }

            return institutionAppointment;
        }

        // PUT: api/InstitutionAppointments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstitutionAppointment(long id, InstitutionAppointment institutionAppointment)
        {
            if (id != institutionAppointment.Id)
            {
                return BadRequest();
            }

            _context.Entry(institutionAppointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitutionAppointmentExists(id))
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

        // POST: api/InstitutionAppointments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<InstitutionAppointment>> PostInstitutionAppointment(InstitutionAppointment institutionAppointment)
        {
            _context.INSTITUTION_APPOINTMENT.Add(institutionAppointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstitutionAppointment", new { id = institutionAppointment.Id }, institutionAppointment);
        }

        // DELETE: api/InstitutionAppointments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InstitutionAppointment>> DeleteInstitutionAppointment(long id)
        {
            var institutionAppointment = await _context.INSTITUTION_APPOINTMENT.FindAsync(id);
            if (institutionAppointment == null)
            {
                return NotFound();
            }

            _context.INSTITUTION_APPOINTMENT.Remove(institutionAppointment);
            await _context.SaveChangesAsync();

            return institutionAppointment;
        }

        private bool InstitutionAppointmentExists(long id)
        {
            return _context.INSTITUTION_APPOINTMENT.Any(e => e.Id == id);
        }
    }
}
