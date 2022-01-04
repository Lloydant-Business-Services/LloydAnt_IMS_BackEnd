using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteHR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteHR.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentTypeController : ControllerBase
    {
        private readonly HRContext _context;

        public AppointmentTypeController(HRContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentType>>> GetAPPOINTMENTTYPE()
        {
            return await _context.APPOINTMENT_TYPE.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentType>> GetAppointmentType(long id)
        {
            var institutionStaffType = await _context.APPOINTMENT_TYPE.FindAsync(id);

            if (institutionStaffType == null)
            {
                return NotFound();
            }

            return institutionStaffType;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppointmentType>> PutAppointmentType(long id, AppointmentType appointmentType)
        {
            if (id != appointmentType.Id)
            {
                return BadRequest();
            }

            _context.APPOINTMENT_TYPE.Update(appointmentType);
            await _context.SaveChangesAsync();
            return appointmentType;
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentType>> PostInstitutionStaffType(AppointmentType appointmentType)
        {
            _context.APPOINTMENT_TYPE.Add(appointmentType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointmentType", new { id = appointmentType.Id }, appointmentType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAppointmentType(long id)
        {
            var appointmentType = await _context.APPOINTMENT_TYPE.FindAsync(id);
            if (appointmentType == null)
            {
                return NotFound();
            }

            _context.APPOINTMENT_TYPE.Remove(appointmentType);
            await _context.SaveChangesAsync();

            return StatusCodes.Status200OK;
        }

        private bool AppointmentTypeExists(long id)
        {
            return _context.APPOINTMENT_TYPE.Any(e => e.Id == id);
        }
    }
}